namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using GameEllipse = AwaitEvents.GameEllipse;

    /// <summary>
    /// Interaction logic for AsyncGame.xaml
    /// </summary>
    public partial class AsyncGame : Window
    {
        const double height = 28.0;
        const double width = 28.0;

        public AsyncGame()
        {
            InitializeComponent();
            Loaded += FinishedLoading;
        }

        private async void FinishedLoading(object sender, RoutedEventArgs e)
        {
            var ellipses = CreateEllipses();
            ShuffleEllipses(ellipses);
            await start.WhenClicked();
            await AwaitEvents.FullGame(ellipses);
        }

        List<GameEllipse> CreateEllipses(int count = 10)
        {
            var ellipses = new List<GameEllipse>(count);
            GameEllipse.SetBoundary(Width, Height);

            for (int i = 0; i < count; i++)
            {
                var ellipse = new Ellipse
                {
                    Fill = Brushes.Yellow,
                    Width = width,
                    Height = height,
                    Stroke = Brushes.DarkGray,
                    StrokeThickness = 0.5
                };
                area.Children.Add(ellipse);
                ellipses.Add(new GameEllipse { Shape = ellipse });
            }

            return ellipses;
        }

        void ShuffleEllipses(IEnumerable<GameEllipse> ellipses)
        {
            var r = new Random();

            foreach (var ellipse in ellipses)
            {
                ellipse.X = r.NextDouble() * Width - width * 0.5;
                ellipse.Y = r.NextDouble() * Height - height * 0.5;
                ellipse.Vx = r.NextDouble() * 5 - 2.5;
                ellipse.Vy = r.NextDouble() * 5 - 2.5;
                Canvas.SetLeft(ellipse.Shape, ellipse.X);
                Canvas.SetBottom(ellipse.Shape, ellipse.Y);
            }
        }
    }
}
