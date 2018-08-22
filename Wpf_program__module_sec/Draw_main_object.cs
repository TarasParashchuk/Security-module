using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wpf_program__module_sec
{
    class Draw_main_object
    {
        Canvas main_canvas;
        public Draw_main_object(Canvas canvas)
        {
            main_canvas = canvas;
        }

        public void Draw_main_line(string[] point_list, Color color, bool flag)
        {
            double[] mass;
            var shapes = new Main_figures(main_canvas);
            var const_coord_y = (0.55 + 10 * 0.8/ 21) * main_canvas.Height;
            var const_coord_x = main_canvas.Width * 0.5 + (8 * main_canvas.Height) / 21;

            for (int i = 0; i < point_list.Length - 1; i = i + 2)
            {
                if (flag)
                    mass = new[] { Convert.ToDouble(point_list[i]), Convert.ToDouble(point_list[i + 1]), Convert.ToDouble(point_list[i]), const_coord_y };
                else
                    mass = new[] { Convert.ToDouble(point_list[i]), Convert.ToDouble(point_list[i + 1]), const_coord_x, Convert.ToDouble(point_list[i + 1]) };

                shapes.Draw_point(Convert.ToDouble(point_list[i + 1]) - 2.5, Convert.ToDouble(point_list[i]) - 2.5, color);
                shapes.Draw_polyline(mass, 1, color, true);
            }
        }

        public void Draw_main_point(string[] point_gr1, string[] point_gr2, Color color)
        {
            var shapes = new Main_figures(main_canvas);

            for (int i = 0; i < point_gr1.Length - 1; i = i + 2)
            {
                for (int j = 0; j < point_gr2.Length - 1; j = j + 2)
                {
                    shapes.Draw_point(Convert.ToDouble(point_gr2[j + 1]) - 2.5, Convert.ToDouble(point_gr1[i]) - 2.5, color);
                }
            }
        }


    }
}
