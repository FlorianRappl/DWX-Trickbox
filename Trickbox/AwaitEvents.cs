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

    /// <summary>
    /// This class contains the (nearly) complete code for a game.
    /// Most importantly the game's (interaction) logic is contained
    /// within a single method.
    /// </summary>
    class AwaitEvents
    {
        public static async Task FullGame(IEnumerable<GameEllipse> ellipses)
        {
            // Some setup
            var r = new Random();
            var active = new List<GameEllipse>(ellipses);
            var cts = new CancellationTokenSource();
            var movingEllipses = MoveEllipsesAsync(ellipses, cts.Token);

            // The game "loop"
            while (active.Count > 0)
            {
                // Select an "active" Ellipse (randomly) and make it red
                var index = r.Next(0, active.Count);
                var selected = active[index];
                selected.Shape.Fill = Brushes.Red;
                // Using the state machinery provided by the Async extensions
                await selected.Shape.WhenHit();
                selected.Shape.Fill = Brushes.Yellow;
                // Hide the active one (it has been hit) and remove it
                selected.Shape.Visibility = Visibility.Hidden;
                active.RemoveAt(index);
            }
            
            // Stop the moving
            cts.Cancel();
        }

        static async Task MoveEllipsesAsync(IEnumerable<GameEllipse> ellipses, CancellationToken cancel)
        {
            // As long as no cancellation is requested we are fine
            while (!cancel.IsCancellationRequested)
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

        /// <summary>
        /// Here we define the properties of our game object
        /// </summary>
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

    /// <summary>
    /// This is where the fun starts. Asynchronous event listeners.
    /// </summary>
    static class AwaitEventExtensions
    {
        public static async Task WhenHit(this Ellipse ellipse)
        {
            var ev = default(MouseButtonEventHandler);
            var tcs = new TaskCompletionSource<bool>();

            // Once the event is triggered we consider the task finished
            ev = (sender, evt) => tcs.TrySetResult(true);

            try
            {
                // Set the listener; crucial
                ellipse.MouseDown += ev;
                // "Wait" (non-blocking) here until the event is fired
                await tcs.Task;
            }
            finally
            {
                // Remove the event listener; prevent memory leaks
                ellipse.MouseDown -= ev;
            }
        }

        /// <summary>
        /// Basically the same, just for a Button and with "Click"
        /// </summary>
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
