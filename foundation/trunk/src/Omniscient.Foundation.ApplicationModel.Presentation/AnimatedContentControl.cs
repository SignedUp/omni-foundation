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
using System.Windows.Markup;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Initial Code from Martin Grayson, http://blogs.msdn.com/mgrayson
    ///
    /// Base class for a control whose size and position animate by using a hidden animating element
    /// and a timer to update the controls size and position to match the animated elements size
    /// and position
    /// </summary>
    public abstract class AnimatedContentControl : ContentControl
    {
        #region Private memebers
        private SplineDoubleKeyFrame sizeAnimationWidthKeyFrame;
        private SplineDoubleKeyFrame sizeAnimationHeightKeyFrame;
        private SplineDoubleKeyFrame positionAnimationXKeyFrame;
        private SplineDoubleKeyFrame positionAnimationYKeyFrame;
        private Rectangle animatedElement;
        private System.Windows.Threading.DispatcherTimer timer;
        private Storyboard sizeAnimation;
        private Storyboard positionAnimation;
        private bool sizeAnimating = false;
        private bool positionAnimating = false;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public AnimatedContentControl()
        {
            // create the animated element XAML
            string animatedElementXaml = "";
            animatedElementXaml += "<Canvas xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>";
            animatedElementXaml += "  <Canvas.Resources>";
            animatedElementXaml += "      <Storyboard x:Key='sizeStoryboard' BeginTime='00:00:00'>";
            animatedElementXaml += "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(FrameworkElement.Width)'> ";
            animatedElementXaml += "              <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />";
            animatedElementXaml += "           </DoubleAnimationUsingKeyFrames>";
            animatedElementXaml += "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(FrameworkElement.Height)'> ";
            animatedElementXaml += "             <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />";
            animatedElementXaml += "          </DoubleAnimationUsingKeyFrames>";
            animatedElementXaml += "      </Storyboard>";
            animatedElementXaml += "      <Storyboard x:Key='positionStoryboard' BeginTime='00:00:00'>";
            animatedElementXaml += "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(Canvas.Left)'> ";
            animatedElementXaml += "              <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />";
            animatedElementXaml += "          </DoubleAnimationUsingKeyFrames>";
            animatedElementXaml += "          <DoubleAnimationUsingKeyFrames Storyboard.TargetName='animatedElement' Storyboard.TargetProperty='(Canvas.Top)'> ";
            animatedElementXaml += "              <SplineDoubleKeyFrame Value='0' KeyTime='00:00:0.5' KeySpline='0.528,0,0.142,0.847' />";
            animatedElementXaml += "          </DoubleAnimationUsingKeyFrames>";
            animatedElementXaml += "      </Storyboard>";
            animatedElementXaml += "  </Canvas.Resources>";
            animatedElementXaml += "  <Rectangle x:Name='animatedElement' Height='0' Width='0' Canvas.Top='0' Canvas.Left='0' />";
            animatedElementXaml += "</Canvas>";

            // Create a canvas from the XAML
            Canvas animatedElement = XamlReader.Load(new System.Xml.XmlTextReader(new System.IO.StringReader(animatedElementXaml))) as Canvas;

            // Pull out the rectangle into a memeber
            this.animatedElement = animatedElement.Children[0] as Rectangle;

            // Pull out the storyboards into a member and hook up completed events
            this.sizeAnimation = animatedElement.Resources["sizeStoryboard"] as Storyboard;
            this.sizeAnimation.Completed += new EventHandler(sizeAnimation_Completed);
            
            this.positionAnimation = animatedElement.Resources["positionStoryboard"] as Storyboard;
            this.positionAnimation.Completed += new EventHandler(positionAnimation_Completed);

            // Pull out the key frames into a member
            this.sizeAnimationWidthKeyFrame =
                ((DoubleAnimationUsingKeyFrames)this.sizeAnimation.Children[0]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.sizeAnimationHeightKeyFrame =
                ((DoubleAnimationUsingKeyFrames)this.sizeAnimation.Children[1]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.positionAnimationXKeyFrame =
                ((DoubleAnimationUsingKeyFrames)this.positionAnimation.Children[0]).KeyFrames[0] as SplineDoubleKeyFrame;
            this.positionAnimationYKeyFrame =
                ((DoubleAnimationUsingKeyFrames)this.positionAnimation.Children[1]).KeyFrames[0] as SplineDoubleKeyFrame;

            // Set up the timer
            this.timer = new System.Windows.Threading.DispatcherTimer();
            this.timer.Tick += new EventHandler(timer_Tick);
        }

        #region Public methods
        /// <summary>
        /// Animates the size of the control
        /// </summary>
        /// <param name="width">The target width</param>
        /// <param name="height">The target height</param>
        public void AnimateSize(double width, double height)
        {
            // Check to ensure the animation stuff has been created
            if (this.animatedElement != null)
            {
                if (this.sizeAnimating) this.sizeAnimation.Pause(this.animatedElement);
                this.animatedElement.Width = this.ActualWidth;
                this.animatedElement.Height = this.ActualHeight;

                // Ensure we are in the tree
                if (this.Parent != null)
                {
                    this.sizeAnimating = true;
                    this.sizeAnimationWidthKeyFrame.Value = width;
                    this.sizeAnimationHeightKeyFrame.Value = height;
                    this.sizeAnimation.Begin(this.animatedElement);
                    this.timer.Start();
                }
                else // if not in the tree, don't animate
                {
                    this.Width = width;
                    this.Height = height;
                }
            }
            else // if animation stuff hasnt been set up, just set
            {
                this.Width = width;
                this.Height = height;
            }
        }


        /// <summary>
        /// Animates the Canvas.Left and Canvas.Top of the control
        /// </summary>
        /// <param name="x">New X position</param>
        /// <param name="y">New Y position</param>
        public void AnimatePosition(double x, double y)
        {
            // Check to ensure the animation stuff has been created
            if (this.animatedElement != null)
            {
                if (this.positionAnimating) this.positionAnimation.Pause(this.animatedElement);
                Canvas.SetLeft(this.animatedElement, Canvas.GetLeft(this));
                Canvas.SetTop(this.animatedElement, Canvas.GetTop(this));

                // Ensure we are in the tree
                if (this.Parent != null)
                {
                    this.positionAnimating = true;
                    this.positionAnimationXKeyFrame.Value = x;
                    this.positionAnimationYKeyFrame.Value = y;

                    this.positionAnimation.Begin(this.animatedElement);
                    this.timer.Start();
                }
                else // if not in the tree, don't animate
                {
                    Canvas.SetLeft(this, x);
                    Canvas.SetTop(this, y);
                }
            }
            else // if animation stuff hasnt been set up, just set
            {
                Canvas.SetLeft(this, x);
                Canvas.SetTop(this, y);
            }
        }
        #endregion

        #region Public override methods
        /// <summary>
        /// Fires when the size animation completes
        /// </summary>
        public virtual void SizeAnimationCompleted()
        {
        }

        /// <summary>
        /// Fires when the position animation completes
        /// </summary>
        public virtual void PositionAnimationCompleted()
        {
        }
        #endregion

        #region Public members
        /// <summary>
        /// The size animation duration
        /// </summary>
        private TimeSpan sizeAnimationTimespan = new TimeSpan(0, 0, 0, 0, 500);
        public TimeSpan SizeAnimationDuration
        {
            get { return sizeAnimationTimespan; }
            set
            {
                this.sizeAnimationTimespan = value;
                if (this.sizeAnimationWidthKeyFrame != null)
                    this.sizeAnimationWidthKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.sizeAnimationTimespan);
                if (this.sizeAnimationHeightKeyFrame != null)
                    this.sizeAnimationHeightKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.sizeAnimationTimespan);
            }
        }

        /// <summary>
        /// The position animation duration
        /// </summary>
        private TimeSpan positionAnimationTimespan = new TimeSpan(0, 0, 0, 0, 500);
        public TimeSpan PositionAnimationDuration
        {
            get { return positionAnimationTimespan; }
            set
            {
                this.positionAnimationTimespan = value;
                if (this.positionAnimationXKeyFrame != null)
                    this.positionAnimationXKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.positionAnimationTimespan);
                if (this.positionAnimationYKeyFrame != null)
                    this.positionAnimationYKeyFrame.KeyTime = KeyTime.FromTimeSpan(this.positionAnimationTimespan);
            }
        }
        #endregion

        #region Private event handlers
        /// <summary>
        /// Handles the size animation completed event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void sizeAnimation_Completed(object sender, EventArgs e)
        {
            // Set size animating to false
            this.sizeAnimating = false;

            // Update the control size one more time
            this.Width = this.animatedElement.Width;
            this.Height = this.animatedElement.Height;

            // If all animations have finished, stop the timer
            if (!this.sizeAnimating && !this.positionAnimating)
                this.timer.Stop();

            // Call size animation completed
            this.SizeAnimationCompleted();
        }

        /// <summary>
        /// Handles the position animation completed event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void positionAnimation_Completed(object sender, EventArgs e)
        {
            // Set position animating to fakse
            this.positionAnimating = false;

            // Update the control position one more time
            Canvas.SetLeft(this, Canvas.GetLeft(this.animatedElement));
            Canvas.SetTop(this, Canvas.GetTop(this.animatedElement));

            // If all animations have finished, stop the timer
            if (!this.sizeAnimating && !this.positionAnimating)
                this.timer.Stop();

            // Call position animation completed
            this.PositionAnimationCompleted();
        }

        /// <summary>
        /// Handles the timer tick to update size and position
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        void timer_Tick(object sender, EventArgs e)
        {
            // If the size is animating, update the controls size
            if (this.sizeAnimating)
            {
                this.Width = this.animatedElement.Width;
                this.Height = this.animatedElement.Height;
            }

            // If the position is animating, update the control position
            if (this.positionAnimating)
            {
                Canvas.SetLeft(this, Canvas.GetLeft(this.animatedElement));
                Canvas.SetTop(this, Canvas.GetTop(this.animatedElement));
            }
        }
        #endregion
    }
}
