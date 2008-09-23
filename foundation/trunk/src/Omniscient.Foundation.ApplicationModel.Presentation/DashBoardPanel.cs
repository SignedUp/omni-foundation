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
using System.Windows.Controls.Primitives;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Initial Code from Martin Grayson, http://blogs.msdn.com/mgrayson
    /// Dashboard Panel
    /// </summary>
    public class DashBoardPanel : AnimatedContentControl, IView
    {

        private bool isDragging;
        private bool isMaximized;
        private Point lastDragPosition;
        static int currentZIndex;        
        private bool ignoreUnCheckedEvent;

        public event DragEventHander DragStarted;
        public event DragEventHander DragMoved;
        public event DragEventHander DragFinished;
        public event EventHandler Maximized;
        public event EventHandler Minimized;

        public DashBoardPanel()
        {
            this.DefaultStyleKey = typeof(DashBoardPanel);
            
            DraggingEnabled = true;
            isDragging = false;
            isMaximized = false;
            currentZIndex = 1;
            ignoreUnCheckedEvent = false;
        }

        public DashBoardPanel(IView myView)
        {
            this.Content = myView;
        }

        /// <summary>
        /// Sets whether dragging is enable on the panel or not
        /// </summary>
        public bool DraggingEnabled { get; set; }

        /// <summary>
        /// Whether or not the panel is maximised (and updates the toggle button UI)
        /// </summary>
        public bool IsMaximized
        {
            get { return this.isMaximized; }
            set
            {
                this.isMaximized = value;
                ToggleButton maximizeToggle = this.GetTemplateChild("PART_MaximizeToggle") as ToggleButton;

                if (maximizeToggle != null)
                {
                    this.ignoreUnCheckedEvent = true;
                    maximizeToggle.IsChecked = this.isMaximized;
                }
            }
        }

        /// <summary>
        /// Gets called once the template is applied so we can fish out the bits
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FrameworkElement gripBar =
                this.GetTemplateChild("PART_GripBar") as FrameworkElement;

            if (gripBar != null)
            {
                gripBar.MouseLeftButtonDown += new MouseButtonEventHandler(gripBar_MouseLeftButtonDown);
                gripBar.MouseMove += new MouseEventHandler(gripBar_MouseMove);
                gripBar.MouseLeftButtonUp += new MouseButtonEventHandler(gripBar_MouseLeftButtonUp);
            }

            ToggleButton maximizeToggle = this.GetTemplateChild("PART_MaximizeToggle") as ToggleButton;

            if (maximizeToggle != null)
            {
                maximizeToggle.Checked += new RoutedEventHandler(maximizeToggle_Checked);
                maximizeToggle.Unchecked += new RoutedEventHandler(maximizeToggle_Unchecked);
            }
        }

        /// <summary>
        /// Fires when the maximize toggle is unchecked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void maximizeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!this.ignoreUnCheckedEvent)
            {
                this.IsMaximized = false;

                // Fire the panel minimized event
                if (this.Minimized != null)
                    this.Minimized(this, e);
            }
            else
            {
                this.ignoreUnCheckedEvent = false;
            }
        }

        /// <summary>
        /// Fires when the maxmize toggle is checked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void maximizeToggle_Checked(object sender, RoutedEventArgs e)
        {
            // Bring the panel to the front
            Canvas.SetZIndex(this, currentZIndex++);

            this.ignoreUnCheckedEvent = false;

            // Fire the panel maximized event
            if (this.Maximized != null)
                this.Maximized(this, e);
        }

        /// <summary>
        /// Fires when the left mouse button goes down on the grip bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gripBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DraggingEnabled)
            {
                // Bring the panel to the front
                Canvas.SetZIndex(this, currentZIndex++);

                // Capture the mouse
                ((FrameworkElement)sender).CaptureMouse();

                // Store the start position
                this.lastDragPosition = e.GetPosition(sender as UIElement);

                // Set dragging to true
                this.isDragging = true;

                // Fire the drag started event
                if (this.DragStarted != null)
                    this.DragStarted(this, new DragEventArgs(0, 0, e));
            }

        }

        /// <summary>
        /// Fires when the left mouse button goes up on the grip bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gripBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.DraggingEnabled)
            {
                // Capture the mouse
                ((FrameworkElement)sender).ReleaseMouseCapture();

                // Set dragging to true
                this.isDragging = false;

                Point position = e.GetPosition(sender as UIElement);

                // Fire the drag finished event
                if (this.DragFinished != null)
                    this.DragFinished(
                        this,
                        new DragEventArgs(
                            position.X - this.lastDragPosition.X,
                            position.Y - this.lastDragPosition.Y, e));
            }
        }

        /// <summary>
        /// Fires when the mouse moves on the grip bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gripBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging)
            {
                Point position = e.GetPosition(sender as UIElement);

                // Move the panel
                Canvas.SetLeft(
                    this,
                    Canvas.GetLeft(this) + position.X - this.lastDragPosition.X
                    );

                Canvas.SetTop(
                    this,
                    Canvas.GetTop(this) + position.Y - this.lastDragPosition.Y
                    );

                // Fire the drag moved event
                if (this.DragMoved != null)
                    this.DragMoved(
                        this,
                        new DragEventArgs(
                            position.X - this.lastDragPosition.X,
                            position.Y - this.lastDragPosition.Y, e));

            }
        }

        #region IView Members

        public IModel Model
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void UpdateView()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
