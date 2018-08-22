using System;

namespace Wpf_program__module_sec
{
    class Intersection_point
    {
        double[] a = new double[2];
        double[] b = new double[2];
        double[] c = new double[2];
        double y1, y2, y3, y4;
        double x1, x2, x3, x4;
        string str;
        double lim_left_p, lim_right_p, lim1;
        bool flg;

        public Intersection_point(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, string lim, bool flag)
        {
            this.y1 = y1;
            this.y2 = y2;
            this.y3 = y3;
            this.y4 = y4;
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
            this.x4 = x4;

            flg = flag;
            str = lim;

            if(str != String.Empty)
            {
                string[] gran = lim.Split(';');
                lim_left_p = Convert.ToDouble(gran[0]);
                lim_right_p = Convert.ToDouble(gran[1]);
                lim1 = Convert.ToDouble(gran[2]);
            }
            
            //Коефіцієнт a при x
            a[0] = y2 - y1;
            a[1] = y4 - y3;
            //Коефіцієнт b при y
            b[0] = x1 - x2;
            b[1] = x3 - x4;
            //Вільний коефіцієнт с
            c[0] = y1 * x2 - x1 * y2;
            c[1] = y3 * x4 - x3 * y4;
        }

        public string Intersection_point_xy()
        {
            //Основний визначник
            double d = a[0] * b[1] - a[1] * b[0];

            if (d == 0)
                return String.Empty;
            else
            {
                // Знаходимо допоміжні визначники
                double d1 = c[0] * b[1] - c[1] * b[0];
                double d2 = a[0] * c[1] - a[1] * c[0];

                double x = -d1 / d;
                double y = -d2 / d + 1;

                if (!flg && (y > Math.Min(y1, y2) && y < Math.Max(y1, y2)) && (y > Math.Min(y3, y4) && y < Math.Max(y3, y4)))
                {
                    if (str != String.Empty)
                    if ((lim_left_p <= y && lim_right_p >= y) && (Math.Min(x1, x2) == lim1 && x > Math.Min(x1, x2) && x < Math.Max(x1, x2)) && (Math.Min(x3, x4) == lim1 && x > Math.Min(x3, x4) && x < Math.Max(x3, x4)))
                        return x.ToString("0.0000") + ";" + y.ToString("0.0000") + ";";
                    else return String.Empty;
                    else return x.ToString("0.0000") + ";" + y.ToString("0.0000") + ";";
                }
                else if (flg && (x > Math.Min(x1, x2) && x < Math.Max(x1, x2)) && (x > Math.Min(x3, x4) && x < Math.Max(x3, x4)))
                {
                    if (str != String.Empty)
                        if ((lim_left_p <= x && lim_right_p >= x) && (Math.Min(y1, y2) == lim1 && y > Math.Min(y1, y2) && y < Math.Max(y1, y2)) && (Math.Min(y3, y4) == lim1 && y > Math.Min(y3, y4) && y < Math.Max(y3, y4)))
                            return x.ToString("0.0000") + ";" + y.ToString("0.0000") + ";";
                        else return String.Empty;
                    else return x.ToString("0.0000") + ";" + y.ToString("0.0000") + ";";
                }
                else return String.Empty;
            }
        }
    }
}
