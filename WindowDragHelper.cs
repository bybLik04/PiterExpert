using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PiterExp
{
    internal class WindowDragHelper
    {
        private bool isDragging = false;
        private Point startPoint;

        public void Attach(Window window)
        {
            window.PreviewMouseDown += Window_PreviewMouseDown;
            window.PreviewMouseMove += Window_PreviewMouseMove;
            window.PreviewMouseUp += Window_PreviewMouseUp;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Window window = sender as Window;

            if (e.LeftButton == MouseButtonState.Pressed && e.GetPosition(window).Y <= SystemParameters.CaptionHeight)
            {
                startPoint = e.GetPosition(window);
                isDragging = true;
            }
        }

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Window window = sender as Window;

            if (isDragging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(window);
                Vector moveDifference = startPoint - currentPosition;

                if (Math.Abs(moveDifference.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(moveDifference.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    window.Left -= moveDifference.X;
                    window.Top -= moveDifference.Y;
                }
            }
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }
    }
}
