namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    class AwaitEvents
    {
        public static async Task FullGame(IEnumerable<GameEllipse> ellipses)
        {
            var r = new Random();
            var active = new List<GameEllipse>(ellipses);
            var cts = new CancellationTokenSource();
            var movingEllipses = MoveEllipsesAsync(ellipses, cts);

            while (active.Count > 0)
            {
                var index = r.Next(0, active.Count);
                var selected = active[index];
                selected.Shape.Fill = Brushes.Red;
                await selected.Shape.WhenHit();
                selected.Shape.Fill = Brushes.Yellow;
                selected.Shape.Visibility = Visibility.Hidden;
                active.RemoveAt(index);
            }
            
            cts.Cancel();
        }

        static async Task MoveEllipsesAsync(IEnumerable<GameEllipse> ellipses, CancellationTokenSource cancel)
        {
            while (!cancel.Token.IsCancellationRequested)
            {
                foreach (var ellipse in ellipses)
                {
                    ellipse.UpdatePosition();
                    Canvas.SetLeft(ellipse.Shape, ellipse.X);
                    Canvas.SetBottom(ellipse.Shape, ellipse.Y);
                }

                await Task.Delay(10);
            }
        }

        public class GameEllipse
        {
            static double _width;
            static double _height;

            public Ellipse Shape { get; set; }

            public double X { get; set; }

            public double Y { get; set; }

            public double Vx { get; set; }

            public double Vy { get; set; }

            public void UpdatePosition()
            {
                X += Vx;
                Y += Vy;

                if (X <= 0 || X >= _width)
                    Vx = -Vx;

                if (Y <= 0 || Y >= _height)
                    Vy = -Vy;
            }

            public static void SetBoundary(double width, double height)
            {
                _width = width;
                _height = height;
            }
        }
    }

    static class AwaitEventExtensions
    {
        public static async Task WhenHit(this Ellipse ellipse)
        {
            var ev = default(MouseButtonEventHandler);
            var tcs = new TaskCompletionSource<bool>();

            ev = (sender, evt) => tcs.TrySetResult(true);

            try
            {
                ellipse.MouseDown += ev;
                await tcs.Task;
            }
            finally
            {
                ellipse.MouseDown -= ev;
            }
        }

        public static async Task WhenClicked(this Button button)
        {
            var ev = default(RoutedEventHandler);
            var tcs = new TaskCompletionSource<bool>();

            ev = (sender, evt) => tcs.TrySetResult(true);

            try
            {
                button.Click += ev;
                await tcs.Task;
            }
            finally
            {
                button.Click -= ev;
            }
        }
    }
}
