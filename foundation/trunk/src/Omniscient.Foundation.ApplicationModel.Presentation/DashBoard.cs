using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Initial Code from Martin Grayson, http://blogs.msdn.com/mgrayson
    /// Dashboard Control
    /// </summary>
    public class DashBoard : Canvas
    {
        private IDashBoardAnimation _animationState;

        /// <summary>
        /// Constructor
        /// </summary>
        public DashBoard()
        {
            this.Loaded += 
                new RoutedEventHandler(DashBoard_Loaded);
            this.SizeChanged += 
                new SizeChangedEventHandler(DashBoard_SizeChanged);

            this.Rows = 1;
            this.Columns = 1;
            this.DraggingPanel = null;
            this.MaximizedPanel = null;
            this.MinimizedColumnWidth = 250;
            this.PanelMargin = new Thickness(10);
            _animationState = new DashBoardMinimizedAnimation(this);
        }

        /// <summary>
        /// Sets the width for the minimzed column on the right side
        /// </summary>
        public double MinimizedColumnWidth { get; set; }
        /// <summary>
        /// Holds the Panel Margin on the dashboard
        /// </summary>
        public Thickness PanelMargin { get; set; }
        /// <summary>
        /// Holds the number of current rows on dashboard
        /// </summary
        internal int Rows { get; set; }
        /// <summary>
        /// Holds the number of current columns on dashboard
        /// </summary
        internal int Columns { get; set; }
        /// <summary>
        /// Holds a reference to the panel which is currently being dragged around on the dashboard
        /// </summary
        internal DashBoardPanel DraggingPanel { get; set; }
        /// <summary>
        /// Holds a reference to the panel which is currently maximized on the dashboard
        /// </summary
        internal DashBoardPanel MaximizedPanel { get; set; }

        public void OpenPanel(IView view)
        {
            DashBoardPanel panel = new DashBoardPanel(view);
            panel.Margin = this.PanelMargin;

            this.Children.Add(panel);

            this.LoadAllPanels();
        }

        #region Panel Events
        /// <summary>
        /// Fires when a panel drag is started
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">args</param>
        void DashBoardPanel_DragStarted(object sender, DragEventArgs args)
        {
            DashBoardPanel panel = sender as DashBoardPanel;

            // Keep reference to dragging panel
            this.DraggingPanel = panel;
        }

        /// <summary>
        /// Fires when a panel is moved during a drag
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">args</param>
        void DashBoardPanel_DragMoved(object sender, DragEventArgs args)
        {
            Point mousePosInHost = 
                args.MouseEventArgs.GetPosition(this);
            
            int currentRow = 
                (int)Math.Floor(mousePosInHost.Y / 
                (this.ActualHeight / (double)this.Rows));

            int currentColumn = 
                (int)Math.Floor(mousePosInHost.X / 
                (this.ActualWidth / (double)this.Columns));

            // Stores the panel we will swap with
            DashBoardPanel swapPanel = null;

            // Loop through children to see if there is a panel to swap with
            foreach (UIElement child in this.Children)
            {
                DashBoardPanel panel = child as DashBoardPanel;

                // If the panel is not the dragging panel and is in the current row
                // or current column... mark it as the panel to swap with
                if (panel != this.DraggingPanel &&
                    Grid.GetColumn(panel) == currentColumn &&
                    Grid.GetRow(panel) == currentRow)
                {
                    swapPanel = panel;
                    break;
                }
            }

            // If there is a panel to swap with
            if (swapPanel != null)
            {
                // Store the new row and column
                int draggingPanelNewColumn = Grid.GetColumn(swapPanel);
                int draggingPanelNewRow = Grid.GetRow(swapPanel);

                // Update the swapping panel row and column
                Grid.SetColumn(swapPanel, Grid.GetColumn(this.DraggingPanel));
                Grid.SetRow(swapPanel, Grid.GetRow(this.DraggingPanel));

                // Update the dragging panel row and column
                Grid.SetColumn(this.DraggingPanel, draggingPanelNewColumn);
                Grid.SetRow(this.DraggingPanel, draggingPanelNewRow);

                // Animate the layout to the new positions
                this.AnimatePanelLayout();
            }
        }

        /// <summary>
        /// Fires when a DashBoardPanel is dropped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void DashBoardPanel_DragFinished(object sender, DragEventArgs args)
        {
            // Set dragging panel back to null
            this.DraggingPanel = null;

            // Update the layout (to reset all panel positions)
            this.UpdatePanelLayout();
        }

        /// <summary>
        /// Fires when a panel is minimized
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void DashBoardPanel_Minimized(object sender, EventArgs e)
        {
            // Set max'ed panel to null
            this.MaximizedPanel = null;

            // Instantiate Minimized Dashboard Animation Class
            _animationState = new DashBoardMinimizedAnimation(this);

            // Loop through children to enable dragging
            foreach (UIElement child in this.Children)
            {
                DashBoardPanel panel =
                    child as DashBoardPanel;
                panel.DraggingEnabled = true;
            }

            // Update sizes and layout
            this.AnimatePanelSizes();
            this.AnimatePanelLayout();
        }

        /// <summary>
        /// Fires when a panel is maximized
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void DashBoardPanel_Maximized(object sender, EventArgs e)
        {
            DashBoardPanel maximizedPanel = sender as DashBoardPanel;

            // Store max'ed panel
            this.MaximizedPanel = maximizedPanel;

            // Instantiate Maximized Dashboard Animation Class
            _animationState = new DashBoardMaximizedAnimation(this);

            // Loop through children to disable dragging
            foreach (UIElement child in this.Children)
            {
                DashBoardPanel panel = child as DashBoardPanel;

                panel.DraggingEnabled = false;

                if (panel != this.MaximizedPanel)
                    panel.IsMaximized = false;
            }

            // Update sizes and layout
            this.AnimatePanelSizes();
            this.AnimatePanelLayout();
        }
        #endregion

        #region DashBoard events
        /// <summary>
        /// Fires when the host size changes
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void DashBoard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdatePanelLayout();
        }

        /// <summary>
        /// Fires when the panel loads. Mainly positions and hooks up the children
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void DashBoard_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadAllPanels();
        }
        #endregion

        #region Private layout methods
        /// <summary>
        /// Fires when the panel loads. Mainly positions and hooks up the children
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void LoadAllPanels()
        {
            // Calculate the number of rows and columns required
            this.Rows = (int)Math.Floor(Math.Sqrt((double)this.Children.Count));

            this.Columns = (int)Math.Ceiling((double)this.Children.Count / (double)this.Rows);

            int child = 0;
            // Loop through the rows and columns and assign to children
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    DashBoardPanel panel = this.Children[child] as DashBoardPanel;

                    // Set starting row and column
                    Grid.SetRow(panel, r);
                    Grid.SetColumn(panel, c);

                    // Hook up panel events
                    panel.DragStarted += new DragEventHander(DashBoardPanel_DragStarted);
                    panel.DragFinished += new DragEventHander(DashBoardPanel_DragFinished);
                    panel.DragMoved += new DragEventHander(DashBoardPanel_DragMoved);
                    panel.Maximized += new EventHandler(DashBoardPanel_Maximized);
                    panel.Minimized += new EventHandler(DashBoardPanel_Minimized);

                    child++;

                    // if we are on the last child, break out of the loop
                    if (child == this.Children.Count)
                        break;
                }

                // if we are on the last child, break out of the loop
                if (child == this.Children.Count)
                    break;
            }

            this.UpdatePanelLayout();
        }
        /// <summary>
        /// Updates the panel layout without animation
        /// This does size and position without animation
        /// </summary>
        private void UpdatePanelLayout()
        {
            _animationState.UpdatePanelLayout();
        }

        /// <summary>
        /// Animates the panel sizes
        /// </summary>
        private void AnimatePanelSizes()
        {
            _animationState.AnimatePanelSizes();
        }

        /// <summary>
        /// Animate the panel positions
        /// </summary>
        private void AnimatePanelLayout()
        {
            _animationState.AnimatePanelLayout();
        }
        #endregion

    }
}
