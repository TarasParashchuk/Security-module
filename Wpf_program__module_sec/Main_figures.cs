using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wpf_program__module_sec
{
    class Main_figures
    {
        Canvas main_canvas;

        public Main_figures(Canvas canvas)
        {
            main_canvas = canvas;
        }

        public void Draw_polyline(double[] arr, int size, Color color, bool flag)
        {
            var polyline = new Polyline();
            polyline.Points = new PointCollection();

            for (int i = 0; i < arr.Length; i = i + 2)
                polyline.Points.Add(new Point(arr[i], arr[i + 1]));

            polyline.Stroke = new SolidColorBrush(color);
            if (flag)
                polyline.StrokeDashArray = new DoubleCollection(new double[] { 4 * size, 2 * size });
            polyline.StrokeThickness = size;
            main_canvas.Children.Add(polyline);
        }

        public void Draw_point(double x, double y, Color color)
        {
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = color;
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.Width = 5;
            myEllipse.Height = 5;
            Canvas.SetTop(myEllipse, x);
            Canvas.SetLeft(myEllipse, y);
            main_canvas.Children.Add(myEllipse);
        }

        public void Draw_Rect(double x, double y, double a, double b, Color color, double opacity)
        {
            var rect = new Rectangle();
            rect.Stroke = new SolidColorBrush(color);
            rect.Fill = new SolidColorBrush(color);

            rect.Fill.Opacity = opacity;

            rect.Width = Math.Abs(a);
            rect.Height = Math.Abs(b);
            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
            main_canvas.Children.Add(rect);
        }

        public void Text(double x1, double y1, string text, int size)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = size;
            textBlock.Text = text;
            Canvas.SetLeft(textBlock, x1);
            Canvas.SetTop(textBlock, y1);
            main_canvas.Children.Add(textBlock);
        }
    }
}
