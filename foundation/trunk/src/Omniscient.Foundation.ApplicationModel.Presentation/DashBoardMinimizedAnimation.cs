using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    class DashBoardMinimizedAnimation : IDashBoardAnimation
    {
        DashBoard _dashBoard;
        public DashBoardMinimizedAnimation(DashBoard dashBoard)
        {
            _dashBoard = dashBoard;
        }

        #region IDashBoardAnimation Members

        public void UpdatePanelLayout()
        {
            // Layout children as per rows and columns
            foreach (UIElement child in _dashBoard.Children)
            {
                DashBoardPanel panel = (DashBoardPanel)child;

                Canvas.SetLeft(
                    panel,
                    (Grid.GetColumn(panel) *
                        (_dashBoard.ActualWidth / (double)_dashBoard.Columns))
                    );

                Canvas.SetTop(
                    panel,
                    (Grid.GetRow(panel) *
                        (_dashBoard.ActualHeight / (double)_dashBoard.Rows))
                    );

                panel.Width =
                    (_dashBoard.ActualWidth / (double)_dashBoard.Columns) -
                    panel.Margin.Left - panel.Margin.Right;

                panel.Height =
                    (_dashBoard.ActualHeight / (double)_dashBoard.Rows) -
                    panel.Margin.Top - panel.Margin.Bottom;
            }
        }

        public void AnimatePanelSizes()
        {
            // Animate the panel sizes to row / column sizes
            foreach (UIElement child in _dashBoard.Children)
            {
                DashBoardPanel panel = (DashBoardPanel)child;
                panel.AnimateSize(
                    (_dashBoard.ActualWidth / (double)_dashBoard.Columns) - panel.Margin.Left - panel.Margin.Right,
                    (_dashBoard.ActualHeight / (double)_dashBoard.Rows) - panel.Margin.Top - panel.Margin.Bottom
                    );
            }
        }

        public void AnimatePanelLayout()
        {
            // Loop through children and size to row and columns
            foreach (UIElement child in _dashBoard.Children)
            {
                DashBoardPanel panel = (DashBoardPanel)child;

                if (panel != _dashBoard.DraggingPanel)
                {
                    panel.AnimatePosition(
                        (Grid.GetColumn(panel) *
                        (_dashBoard.ActualWidth / (double)_dashBoard.Columns)),
                        (Grid.GetRow(panel)
                        * (_dashBoard.ActualHeight / (double)_dashBoard.Rows))
                        );
                }
            }
        }

        #endregion
    }
}
