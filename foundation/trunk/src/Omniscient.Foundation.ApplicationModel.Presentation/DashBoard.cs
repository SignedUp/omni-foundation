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
        // A local store of the number of rows
        private int rows;
        // A local store of the number of columns
        private int columns;
        // The panel currently being dragged
        private DashBoardPanel draggingPanel;
        // The currently maxmised panel
        private DashBoardPanel maximizedPanel;

        /// <summary>
        /// Constructor
        /// </summary>
        public DashBoard()
        {
            this.Loaded += 
                new RoutedEventHandler(DashBoard_Loaded);
            this.SizeChanged += 
                new SizeChangedEventHandler(DashBoard_SizeChanged);

            rows = 1;
            columns = 1;
            draggingPanel = null;
            maximizedPanel = null;
            this.MinimizedColumnWidth = 250;
            this.PanelMargin = new Thickness(10);
        }

        /// <summary>
        /// Sets the width for the minimzed column on the right side
        /// </summary>
        public double MinimizedColumnWidth { get; set; }
        public Thickness PanelMargin { get; set; }

        public void OpenPanel(IView view)
        {
            DashBoardPanel panel = new DashBoardPanel(view);
            panel.Margin = this.PanelMargin;

            this.Children.Add(panel);

            this.LoadAll();
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
            this.draggingPanel = panel;
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
                (this.ActualHeight / (double)this.rows));

            int currentColumn = 
                (int)Math.Floor(mousePosInHost.X / 
                (this.ActualWidth / (double)this.columns));

            // Stores the panel we will swap with
            DashBoardPanel swapPanel = null;

            // Loop through children to see if there is a panel to swap with
            foreach (UIElement child in this.Children)
            {
                DashBoardPanel panel = child as DashBoardPanel;

                // If the panel is not the dragging panel and is in the current row
                // or current column... mark it as the panel to swap with
                if (panel != this.draggingPanel &&
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
                Grid.SetColumn(swapPanel, Grid.GetColumn(this.draggingPanel));
                Grid.SetRow(swapPanel, Grid.GetRow(this.draggingPanel));

                // Update the dragging panel row and column
                Grid.SetColumn(this.draggingPanel, draggingPanelNewColumn);
                Grid.SetRow(this.draggingPanel, draggingPanelNewRow);

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
            this.draggingPanel = null;

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
            this.maximizedPanel = null;

            // Loop through children to disable dragging
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
            this.maximizedPanel = maximizedPanel;

            // Loop through children to disable dragging
            foreach (UIElement child in this.Children)
            {
                DashBoardPanel panel = child as DashBoardPanel;

                panel.DraggingEnabled = false;

                if (panel != this.maximizedPanel)
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
            this.LoadAll();
        }
        #endregion

        #region Private layout methods
        /// <summary>
        /// Fires when the panel loads. Mainly positions and hooks up the children
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void LoadAll()
        {
            // Calculate the number of rows and columns required
            this.rows = (int)Math.Floor(Math.Sqrt((double)this.Children.Count));

            this.columns = (int)Math.Ceiling((double)this.Children.Count / (double)rows);

            int child = 0;
            // Loop through the rows and columns and assign to children
            for (int r = 0; r < this.rows; r++)
            {
                for (int c = 0; c < this.columns; c++)
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
            // If we are not in max'ed panel mode...
            if (this.maximizedPanel == null)
            {
                // Layout children as per rows and columns
                foreach (UIElement child in this.Children)
                {
                    DashBoardPanel panel = (DashBoardPanel)child;

                    Canvas.SetLeft(
                        panel,
                        (Grid.GetColumn(panel) *
                            (this.ActualWidth / (double)this.columns))
                        );

                    Canvas.SetTop(
                        panel,
                        (Grid.GetRow(panel) *
                            (this.ActualHeight / (double)this.rows))
                        );

                    panel.Width =
                        (this.ActualWidth / (double)this.columns) -
                        panel.Margin.Left - panel.Margin.Right;

                    panel.Height =
                        (this.ActualHeight / (double)this.rows) -
                        panel.Margin.Top - panel.Margin.Bottom;
                }
            }
            else // If a panel is maxmized
            {
                //List<DashBoardPanel> orderedPanels = new List<DashBoardPanel>();
                Dictionary<int, DashBoardPanel> orderedPanels = new Dictionary<int, DashBoardPanel>();

                // Loop through children to order them according to their
                // current row and column...
                foreach (UIElement child in this.Children)
                {
                    DashBoardPanel panel = (DashBoardPanel)child;

                    orderedPanels.Add(
                        (Grid.GetRow(panel) * this.columns) + Grid.GetColumn(panel),
                        panel);
                }

                // Set initial top of minimized panels to 0
                double currentTop = 0.0;

                // For each of the panels (as ordered in the grid)
                for (int i = 0; i < orderedPanels.Count; i++)
                {
                    // If the current panel is not the maximized panel
                    if (orderedPanels[i] != this.maximizedPanel)
                    {
                        // Set the size of the panel
                        orderedPanels[i].Width = this.MinimizedColumnWidth - orderedPanels[i].Margin.Left - orderedPanels[i].Margin.Right;
                        orderedPanels[i].Height = (this.ActualHeight / (double)(this.Children.Count - 1)) - orderedPanels[i].Margin.Top - orderedPanels[i].Margin.Bottom;

                        // Set the position of the panel
                        Canvas.SetLeft(orderedPanels[i], this.ActualWidth - this.MinimizedColumnWidth);
                        Canvas.SetTop(orderedPanels[i], currentTop);

                        // Increment top
                        currentTop += this.ActualHeight / (double)(this.Children.Count - 1);
                    }
                    else // If the current panel is the maximized panel
                    {
                        // Set the size of the panel
                        orderedPanels[i].Width = this.ActualWidth - this.MinimizedColumnWidth - orderedPanels[i].Margin.Left - orderedPanels[i].Margin.Right;
                        orderedPanels[i].Height = this.ActualHeight - orderedPanels[i].Margin.Top - orderedPanels[i].Margin.Bottom;

                        // Set the position of the panel
                        Canvas.SetLeft(orderedPanels[i], 0);
                        Canvas.SetTop(orderedPanels[i], 0);
                    }
                }
            }
        }

        /// <summary>
        /// Animates the panel sizes
        /// </summary>
        private void AnimatePanelSizes()
        {
            // If there is not a maxmized panel...
            if (this.maximizedPanel == null)
            {
                // Animate the panel sizes to row / column sizes
                foreach (UIElement child in this.Children)
                {
                    DashBoardPanel panel = (DashBoardPanel)child;
                    panel.AnimateSize(
                        (this.ActualWidth / (double)this.columns) - panel.Margin.Left - panel.Margin.Right,
                        (this.ActualHeight / (double)this.rows) - panel.Margin.Top - panel.Margin.Bottom
                        );
                }
            }
            else // If there is a maximized panel
            {
                // Loop through the children
                foreach (UIElement child in this.Children)
                {
                    DashBoardPanel panel = (DashBoardPanel)child;

                    // Set the size of the non 
                    // maximized children
                    if (panel != this.maximizedPanel)
                    {
                        panel.AnimateSize(
                            this.MinimizedColumnWidth - 
                            panel.Margin.Left - 
                            panel.Margin.Right,
                            (this.ActualHeight / 
                            (double)(this.Children.Count - 1)) - 
                            panel.Margin.Top - panel.Margin.Bottom
                            );
                    }
                    else // Set the size of the maximized child
                    {
                        panel.AnimateSize(
                            this.ActualWidth - 
                            this.MinimizedColumnWidth - 
                            panel.Margin.Left - panel.Margin.Right,
                            this.ActualHeight - 
                            panel.Margin.Top - panel.Margin.Bottom
                            );
                    }
                }
            }
        }

        /// <summary>
        /// Animate the panel positions
        /// </summary>
        private void AnimatePanelLayout()
        {
            // If we are not in max'ed panel mode...
            if (this.maximizedPanel == null)
            {
                // Loop through children and size to row and columns
                foreach (UIElement child in this.Children)
                {
                    DashBoardPanel panel = (DashBoardPanel)child;

                    if (panel != this.draggingPanel)
                    {
                        panel.AnimatePosition(
                            (Grid.GetColumn(panel) *
                            (this.ActualWidth / (double)this.columns)),
                            (Grid.GetRow(panel)
                            * (this.ActualHeight / (double)this.rows))
                            );
                    }
                }
            }
            else // If a panel is maximized...
            {
                Dictionary<int, DashBoardPanel> orderedPanels = new Dictionary<int, DashBoardPanel>();

                // Loop through children to order them according to their
                // current row and column...
                foreach (UIElement child in this.Children)
                {
                    DashBoardPanel panel = (DashBoardPanel)child;

                    orderedPanels.Add(
                        (Grid.GetRow(panel) * this.columns) + Grid.GetColumn(panel),
                        panel);
                }

                // Set initial top of minimized panels to 0
                double currentTop = 0.0;

                // For each of the panels (as ordered in the grid)
                for (int i = 0; i < orderedPanels.Count; i++)
                {
                    // If the current panel is not the maximized panel
                    if (orderedPanels[i] != this.maximizedPanel)
                    {
                        // Animate the size
                        orderedPanels[i].AnimatePosition(
                            this.ActualWidth - this.MinimizedColumnWidth,
                            currentTop
                            );

                        // Increment current top
                        currentTop += this.ActualHeight / (double)(this.Children.Count - 1);
                    }
                    else // If the current panel is the maxmized panel
                    {
                        // Animate it to 0,0
                        orderedPanels[i].AnimatePosition(0, 0);
                    }
                }
            }
        }
        #endregion

    }
}
