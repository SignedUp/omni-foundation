using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace System.Windows
{

    /// <summary>
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
	[ContentProperty("Storyboard")]
	[RuntimeNameProperty("Name")]
	public class VisualState : DependencyObject
	{
		private static readonly DependencyProperty StoryboardProperty = DependencyProperty.Register("Storyboard", typeof(Storyboard), typeof(VisualState));
		public Storyboard Storyboard
		{
			get { return (Storyboard)this.GetValue(VisualState.StoryboardProperty); }
			set { this.SetValue(VisualState.StoryboardProperty, value); }
		}

		private string _name;
		public string Name
		{
			get { return this._name; }
			set { this._name = value; }
		}
	}
}
