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

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    /// <summary>
    /// Initial Code from Martin Grayson, http://blogs.msdn.com/mgrayson
    /// Class to represent dragging event arguments
    /// </summary>
    public class DragEventArgs : EventArgs
    {
        /// <summary>
        /// Constuctor
        /// </summary>
        public DragEventArgs() 
        { 
        }

        /// <summary>
        /// Contstructor with bits
        /// </summary>
        /// <param name="hChange">Horizontal change</param>
        /// <param name="vChange">Vertical change</param>
        /// <param name="mouseEventArgs">The mouse event args</param>
        public DragEventArgs(double hChange, double vChange, MouseEventArgs mouseEventArgs) 
        { 
            HorizontalChange = hChange; 
            VerticalChange = vChange; 
            MouseEventArgs = mouseEventArgs; 
        }
        
        /// <summary>
        /// Represents the horizontal change of the drag
        /// </summary>
        public double HorizontalChange { get; set; }
        
        /// <summary>
        /// Represents the vertical change of the drag
        /// </summary>
        public double VerticalChange { get; set; }
        
        /// <summary>
        /// Stores the mouse event args
        /// </summary>
        public MouseEventArgs MouseEventArgs { get; set; }
    }

    /// <summary>
    /// Delegate for creating drag events
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="args">args</param>
    public delegate void DragEventHander(object sender, DragEventArgs args);
}
