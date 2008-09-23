using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ApplicationModel.Presentation
{
    class DashBoardMaximizedAnimation : IDashBoardAnimation
    {
        DashBoard _dashBoard;
        public DashBoardMaximizedAnimation(DashBoard dashBoard)
        {
            _dashBoard = dashBoard;
        }
    
        #region IDashBoardAnimation Members

        public void UpdatePanelLayout()
        {
            Dictionary<int, DashBoardPanel> orderedPanels = new Dictionary<int, DashBoardPanel>();

            // Loop through children to order them according to their current row and column...
            foreach (UIElement child in _dashBoard.Children)
            {
                DashBoardPanel panel = (DashBoardPanel)child;

                orderedPanels.Add((Grid.GetRow(panel) * _dashBoard.Columns) + Grid.GetColumn(panel), panel);
            }

            // Set initial top of minimized panels to 0
            double currentTop = 0.0;

            // For each of the panels (as ordered in the grid)
            for (int i = 0; i < orderedPanels.Count; i++)
            {
                // If the current panel is not the maximized panel
                if (orderedPanels[i] != _dashBoard.MaximizedPanel)
                {
                    // Set the size of the panel
                    orderedPanels[i].Width = _dashBoard.MinimizedColumnWidth - orderedPanels[i].Margin.Left - orderedPanels[i].Margin.Right;
                    orderedPanels[i].Height = (_dashBoard.ActualHeight / (double)(_dashBoard.Children.Count - 1)) - orderedPanels[i].Margin.Top - orderedPanels[i].Margin.Bottom;

                    // Set the position of the panel
                    Canvas.SetLeft(orderedPanels[i], _dashBoard.ActualWidth - _dashBoard.MinimizedColumnWidth);
                    Canvas.SetTop(orderedPanels[i], currentTop);

                    // Increment top
                    currentTop += _dashBoard.ActualHeight / (double)(_dashBoard.Children.Count - 1);
                }
                else // If the current panel is the maximized panel
                {
                    // Set the size of the panel
                    orderedPanels[i].Width = _dashBoard.ActualWidth - _dashBoard.MinimizedColumnWidth - orderedPanels[i].Margin.Left - orderedPanels[i].Margin.Right;
                    orderedPanels[i].Height = _dashBoard.ActualHeight - orderedPanels[i].Margin.Top - orderedPanels[i].Margin.Bottom;

                    // Set the position of the panel
                    Canvas.SetLeft(orderedPanels[i], 0);
                    Canvas.SetTop(orderedPanels[i], 0);
                }
            }
        }

        public void AnimatePanelSizes()
        {
            // Loop through the children
            foreach (UIElement child in _dashBoard.Children)
            {
                DashBoardPanel panel = (DashBoardPanel)child;

                // Set the size of the non 
                // maximized children
                if (panel != _dashBoard.MaximizedPanel)
                {
                    panel.AnimateSize(
                        _dashBoard.MinimizedColumnWidth -
                        panel.Margin.Left -
                        panel.Margin.Right,
                        (_dashBoard.ActualHeight /
                        (double)(_dashBoard.Children.Count - 1)) -
                        panel.Margin.Top - panel.Margin.Bottom
                        );
                }
                else // Set the size of the maximized child
                {
                    panel.AnimateSize(
                        _dashBoard.ActualWidth -
                        _dashBoard.MinimizedColumnWidth -
                        panel.Margin.Left - panel.Margin.Right,
                        _dashBoard.ActualHeight -
                        panel.Margin.Top - panel.Margin.Bottom
                        );
                }
            }
        }

        public void AnimatePanelLayout()
        {
            Dictionary<int, DashBoardPanel> orderedPanels = new Dictionary<int, DashBoardPanel>();

            // Loop through children to order them according to their
            // current row and column...
            foreach (UIElement child in _dashBoard.Children)
            {
                DashBoardPanel panel = (DashBoardPanel)child;

                orderedPanels.Add(
                    (Grid.GetRow(panel) * _dashBoard.Columns) + Grid.GetColumn(panel),
                    panel);
            }

            // Set initial top of minimized panels to 0
            double currentTop = 0.0;

            // For each of the panels (as ordered in the grid)
            for (int i = 0; i < orderedPanels.Count; i++)
            {
                // If the current panel is not the maximized panel
                if (orderedPanels[i] != _dashBoard.MaximizedPanel)
                {
                    // Animate the size
                    orderedPanels[i].AnimatePosition(
                        _dashBoard.ActualWidth - _dashBoard.MinimizedColumnWidth,
                        currentTop
                        );

                    // Increment current top
                    currentTop += _dashBoard.ActualHeight / (double)(_dashBoard.Children.Count - 1);
                }
                else // If the current panel is the maxmized panel
                {
                    // Animate it to 0,0
                    orderedPanels[i].AnimatePosition(0, 0);
                }
            }
        }

        #endregion
    }
}
