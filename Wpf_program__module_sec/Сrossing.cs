using System;
using System.Collections.Generic;

namespace Wpf_program__module_sec
{
    public class Сrossing
    {
        public List<Class_point> Intersection_gr_point(List<Class_point_gr> list, string interval_p, bool flag)
        {
            var str = new List<Class_point>();

            for (int index = 0; index < list.Count - 1; index++)
            {
                if (index == 0)
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        var buf = func_point(list, index, i, interval_p, flag);
                        if (buf != String.Empty && str.Count != 2)
                            str.Add(new Class_point() { Name = list[i].Name.ToString(), Index = "1", xy = buf });
                    }
                    var xy = list[index].xy.Split(';');
                    str.Add(new Class_point() { Name = null, Index = "1", xy = Convert.ToDouble(xy[0]) + ";" + Convert.ToDouble(xy[1]) + ";" });
                    str.Add(new Class_point() { Name = null, Index = "1", xy = Convert.ToDouble(xy[xy.Length - 3]) + ";" + Convert.ToDouble(xy[xy.Length - 2]) + ";" });
                }
                else
                {
                    var buf = func_point(list, index, index + 1, String.Empty, flag);
                    if (buf != String.Empty)
                    {
                        str.Add(new Class_point() { Name = list[index].Name.ToString(), Index = "0", xy = buf });
                    }
                }
            }
            return str;
        }

        public string func_point(List<Class_point_gr> list, int index1, int index2, string lim, bool flag)
        {
            var str = String.Empty;
            var xy1 = list[index1].xy.Split(';');
            var xy2 = list[index2].xy.Split(';');

            for (int i = 0; i < xy1.Length - 3; i = i + 2)
            {
                var x1 = Convert.ToDouble(xy1[i]);
                var y1 = Convert.ToDouble(xy1[i + 1]);
                var x2 = Convert.ToDouble(xy1[i + 2]);
                var y2 = Convert.ToDouble(xy1[i + 3]);

                for (int j = 0; j < xy2.Length - 3; j = j + 2)
                {
                    var x3 = Convert.ToDouble(xy2[j]);
                    var y3 = Convert.ToDouble(xy2[j + 1]);
                    var x4 = Convert.ToDouble(xy2[j + 2]);
                    var y4 = Convert.ToDouble(xy2[j + 3]);

                    var point = new Intersection_point(x1, y1, x2, y2, x3, y3, x4, y4, lim, flag);
                    if (point.Intersection_point_xy() != String.Empty)
                    {
                        str += point.Intersection_point_xy();
                        break;
                    }
                }
                if (str != string.Empty)
                    break;
            }
            return str;
        }
    }
}
