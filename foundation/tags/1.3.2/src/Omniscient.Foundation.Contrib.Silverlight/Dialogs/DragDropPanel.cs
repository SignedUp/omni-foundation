using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Omniscient.Foundation.Contrib.Silverlight.Dialogs
{
    public class DragDropPanel : Canvas
    {
        private Point beginP;
        private Point currentP;
        private bool dragOn;

        public DragDropPanel()
        {
            MouseLeftButtonDown += DragDropPanel_MouseLeftButtonDown;
            MouseLeftButtonUp += DragDropPanel_MouseLeftButtonUp;
            MouseMove += DragDropPanel_MouseMove;
            Cursor = Cursors.Hand;
        }

        private void DragDropPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragOn) return;

            currentP = e.GetPosition(null);
            double x0 = System.Convert.ToDouble(GetValue(LeftProperty));
            double y0 = System.Convert.ToDouble(GetValue(TopProperty));
            SetValue(LeftProperty, x0 + currentP.X - beginP.X);
            SetValue(TopProperty, y0 + currentP.Y - beginP.Y);
            beginP = currentP;
        }

        private void DragDropPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!dragOn) return;

            Opacity *= 2;
            ReleaseMouseCapture();
            dragOn = false;
        }

        private void DragDropPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement c = (FrameworkElement) sender;
            dragOn = true;
            beginP = e.GetPosition(null);
            c.Opacity *= 0.5;
            c.CaptureMouse();
        }
    }
}