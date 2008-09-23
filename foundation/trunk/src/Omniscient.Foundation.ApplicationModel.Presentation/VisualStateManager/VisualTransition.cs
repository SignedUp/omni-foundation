using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace System.Windows
{
    /// <summary>
    /// Defines a transition between VisualStates.
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    [ContentProperty("Storyboard")]
    public class VisualTransition : DependencyObject
    {
        /// <summary>
        /// Name of the state to transition from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Name of the state to transition to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Storyboard providing fine grained control of the transition.
        /// </summary>
        public Storyboard Storyboard { get; set; }

        /// <summary>
        /// Duration of the transition.
        /// </summary>
        [TypeConverter(typeof(System.Windows.DurationConverter))]
        public Duration Duration { get { return _duration; } set { _duration = value; } }

        internal bool IsDefault
        {
            get { return From == null && To == null; }
        }

        private Duration _duration = new Duration(new TimeSpan());
    }
}
