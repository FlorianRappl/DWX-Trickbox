namespace Trickbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += FinishedLoading;
        }

        void FinishedLoading(object sender, RoutedEventArgs ev)
        {
            var buttons = FindVisualChildren<Button>(this);

            foreach (var button in buttons)
            {
                var target = button.Name;
                button.Click += (s, e) =>
                {
                    var window = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(m => m.Name == target)
                        .Select(m => m.GetConstructor(Type.EmptyTypes).Invoke(null))
                        .OfType<Window>().FirstOrDefault();
                    window?.Show();
                };
            }
        }

        static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}
