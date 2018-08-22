using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_program__module_sec
{
    class Coordinate_axes
    {
        Main_figures shapes;
        double h, l;

        public Coordinate_axes(Canvas canvas)
        {
            shapes = new Main_figures(canvas);
            h = canvas.Height * 0.5;
            l = canvas.Width * 0.5;
        }

        public void Main_coordinate_axes()
        {
            //Малювання сітки для графіка
            Grid_coordinates(h* 0.9, h* 0.13, l + h* 0.76, l, h* 0.076);
            Grid_coordinates(h* 1.86, h* 1.1, l* 0.9, l* 0.25, h* 0.076);
            //Графік №1
            //підписання та градуювання осi ОУ Pspkoп
            Graduation_axes(h* 0.9, h* 0.1, -h* 0.076, l, l* 0.95, true);
            //підписання та градуювання осi ОХ Pspkoп
            Graduation_axes(l, l + h* 0.8, h* 0.076, h* 0.9, h* 0.95, false);

            //Графік №2
            //підписання та градуювання осi ОУ Pspkoпa
            Graduation_axes(h* 1.1, h* 1.9, h* 0.076, l* 0.9, l* 0.93, true);
            //підписання та градуювання осi ОХ Pspkoпa
            Graduation_axes(l* 0.9, l* 0.9 - h* 0.8, -h* 0.076, h* 1.1, h* 1.03, false);

            //Графік №1 малювання осі
            Drawing_axes(l, h* 0.9, l + 0.8 * h, l + 0.8 * (h + 15), 0.8 * (h - 5), 0.8 * h, 1.8 * h);

            //Графік №2 малювання осі
            Drawing_axes(-l* 0.9, h* 1.1, l* 0.9 - 0.8 * (h + 15), l* 0.9 - 0.8 * h, 0.8 * (h - 5), 0.8 * h, 0);
        }

        public void Grid_coordinates(double lim_x1, double lim_x2, double lim_y1, double lim_y2, double interval)
        {
            double[] arr;
            for (double i = lim_x1; i > lim_x2; i = i - interval)
            {
                arr = new[] { lim_y1, i, lim_y2, i };
                shapes.Draw_polyline(arr, 1, Colors.LightGray, false);
            }
            for (double i = lim_y1; i > lim_y2; i = i - interval)
            {
                arr = new[] { i, lim_x1, i, lim_x2 };
                shapes.Draw_polyline(arr, 1, Colors.LightGray, false);
            }
        }

        public void Graduation_axes(double a, double b, double interval, double j, double grad, bool index)
        {
            double text = 0.1;
            double[] arr;

            for (double i = a + interval; (i <= b && a < b) || (i >= b && a > b); i = i + interval)
            {
                if (index)
                {
                    arr = new[] { j + 4, i, j - 4, i };
                    shapes.Text(grad, i - 10, text.ToString(), Convert.ToInt32(Math.Abs(interval) * 0.4));
                }
                else
                {
                    arr = new[] { i, j + 4, i, j - 4 };
                    shapes.Text(i, grad, text.ToString(), Convert.ToInt32(Math.Abs(interval) * 0.4));
                }
                shapes.Draw_polyline(arr, 2, Colors.Black, false);
                text = text + 0.1;
            }
        }

        public void Drawing_axes(double x0, double x1, double x2, double x3, double x4, double x5, double x6)
        {
            double[] arr;
            // відмалювання меж для графіків 1-2
            arr = new[] { Math.Abs(x0 + x5), x1, Math.Abs(x0), x1, Math.Abs(x0), Math.Abs(x1 - (x6 - x5)) };
            shapes.Draw_polyline(arr, 2, Colors.Black, false);

            // відмалювання стрілочок для графіків 1-2 вісь ОХ
            arr = new[] { Math.Abs(x0 + x4), x1 - (x5 - x4), Math.Abs(x0 + x5), x1, Math.Abs(x0 + x4), x1 + (x5 - x4) };
            shapes.Draw_polyline(arr, 2, Colors.Black, false);
            // відмалювання стрілочок для графіків 1-2 вісь ОY
            arr = new[] { Math.Abs(x0) - (x5 - x4), Math.Abs(x1 - (x6 - x4)), Math.Abs(x0), Math.Abs(x1 - (x6 - x5)), Math.Abs(x0) + (x5 - x4), Math.Abs(x1 - (x6 - x4)) };
            shapes.Draw_polyline(arr, 2, Colors.Black, false);
        }
    }
}
