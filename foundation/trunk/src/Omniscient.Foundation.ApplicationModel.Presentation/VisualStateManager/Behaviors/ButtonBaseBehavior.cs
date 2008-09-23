using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace System.Windows.Controls.Internal
{
    /// <summary>
    /// http://blogs.msdn.com/johngossman/archive/2008/08/08/visualstatemanager-for-desktop-wpf.aspx
    /// </summary>
    internal class ButtonBaseBehavior: VisualStateBehavior
    {
        public override Type TargetType { get { return typeof(ButtonBase); } }
        public override void Attach(Control control)
        {
            ButtonBase button = (ButtonBase)control;
            DependencyPropertyDescriptor.FromProperty(ButtonBase.IsMouseOverProperty, typeof(ButtonBase)).AddValueChanged(button,
                delegate { Button_UpdateState(button); });
            DependencyPropertyDescriptor.FromProperty(ButtonBase.IsEnabledProperty, typeof(ButtonBase)).AddValueChanged(button,
                delegate { Button_UpdateState(button); });
            DependencyPropertyDescriptor.FromProperty(ButtonBase.IsPressedProperty, typeof(ButtonBase)).AddValueChanged(button,
                delegate { Button_UpdateState(button); });

            button.Loaded += button_Loaded;
        }

        static void button_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonBase button = sender as ButtonBase;
            if (button != null)
            {
                VisualStateManager.GoToState(button, "Normal", false);
            }
        }

        private static void Button_UpdateState(ButtonBase button)
        {
            if (!button.IsEnabled)
            {
                VisualStateManager.GoToState(button, "Disabled", true);
            }
            else if (button.IsPressed)
            {
                VisualStateManager.GoToState(button, "Pressed", true);
            }
            else if (button.IsMouseOver)
            {
                VisualStateManager.GoToState(button, "MouseOver", true);
            }
            else
            {
                VisualStateManager.GoToState(button, "Normal", true);
            }
        }
    }
}
