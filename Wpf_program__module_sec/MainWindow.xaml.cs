using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO.Packaging;
using System.Xml;
using System.IO;

namespace Wpf_program__module_sec
{
    public partial class MainWindow : Window
    {
        double koef = 0.8;
        string[] Name_Oblast_ST = new string[] { "Н", "БНВ", "БВН", "В", "П" };
        string[] Name_Oblast = new string[] { "ОМ", "М", "С", "Б", "ОБ" };
        string[] interval_p = new string[2];
        string gridXaml;
        List<Class_point_gr> list_gr1;
        List<Class_point_gr> list_gr2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            using (var db = new bd_table())
            {
                list_gr1 = new List<Class_point_gr>();
                list_gr2 = new List<Class_point_gr>();

                foreach (Class_table str in db.table.ToList())
                {
                    if (str.index == "Тspkoп")
                        list_gr1.Add(new Class_point_gr() { Id = str.Id, Name = str.Name, xy = str.xy });
                    else list_gr2.Add(new Class_point_gr() { Id = str.Id, Name = str.Name, xy = str.xy });
                }
                data_grid1.ItemsSource = list_gr1;
                data_grid2.ItemsSource = list_gr2;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Class_Model.Buffer = null;
            ChildWindow window = new ChildWindow();
            window.Owner = this;
            window.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ChildWindow window = new ChildWindow();
            Class_point_gr gr;
            if (data_grid1.SelectedItem != null || data_grid2.SelectedItem != null)
            {
                if (data_grid1.SelectedItem != null)
                    gr = data_grid1.SelectedItem as Class_point_gr;
                else gr = data_grid2.SelectedItem as Class_point_gr;
                if (gr != null)
                {
                    Class_Model.Buffer = gr.Id.ToString();
                    window.Owner = this;
                    window.Show();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Class_point_gr gr;
            if (data_grid1.SelectedItem != null || data_grid2.SelectedItem != null)
            {
                using (bd_table db = new bd_table())
                {
                    if (data_grid1.SelectedItem != null)
                        gr = data_grid1.SelectedItem as Class_point_gr;
                    else gr = data_grid2.SelectedItem as Class_point_gr;

                    var item = db.table.ToList().Where(u => u.Id == gr.Id).First();
                    if (item != null)
                    {
                        db.table.Remove(item);
                        db.SaveChanges();
                    }

                    Window_Activated(sender, e);
                }
            }
        }

        public string[] Function_Split(List<Class_point> point_list, int flag)
        {
            var str = String.Empty;
            for (int i = 0; i < point_list.Count; i++)
            {
                if (point_list[i].Index == flag.ToString())
                    str += point_list[i].xy;
            }
            return str.Split(';');
        }

        public void Draw_main_rect(string[] point_gr1, string[] point_gr2, Color[] color, int k0, int k1, int k2, int j)
        {
            //у,х,  ширина, висота
            var shapes = new Main_figures(gr);
            var left_limit_x = gr.Width / 2;
            var left_limit_y = 0.55 * gr.Height;
            var right_limit_x = left_limit_x + (8 * gr.Height) / 21;
            var right_limit_y = left_limit_y + (8 * gr.Height) / 21;
            var count = 0;
            double cord;

            if (k1 != 0)
            {
                cord = Convert.ToDouble(point_gr2[j + 3]) - Convert.ToDouble(point_gr2[j + 1]);
            }
            else cord = 0;

            shapes.Draw_Rect(left_limit_x, left_limit_y + k1 * (Convert.ToDouble(point_gr2[j + 1]) - left_limit_y) + k2 * (Convert.ToDouble(point_gr2[point_gr2.Length - 2]) - left_limit_y), Convert.ToDouble(point_gr1[0]) - left_limit_x, k2 * (right_limit_y - Convert.ToDouble(point_gr2[point_gr2.Length - 2])) + k1 * cord + k0 * (Convert.ToDouble(point_gr2[1]) - left_limit_y), color[count++], 1);
            for (int i = 0; i < point_gr1.Length - 3; i = i + 2)
            {
                shapes.Draw_Rect(Convert.ToDouble(point_gr1[i]), left_limit_y + (k1 * (Convert.ToDouble(point_gr2[j + 1]) - left_limit_y) + k2 * (Convert.ToDouble(point_gr2[point_gr2.Length - 2]) - left_limit_y)), Convert.ToDouble(point_gr1[i + 2]) - Convert.ToDouble(point_gr1[i]), k2 * (right_limit_y - Convert.ToDouble(point_gr2[point_gr2.Length - 2])) + k1 * cord + k0 * (Convert.ToDouble(point_gr2[1]) - left_limit_y), color[count++], 1);
            }
            shapes.Draw_Rect(Convert.ToDouble(point_gr1[point_gr1.Length - 3]), left_limit_y + k1 * (Convert.ToDouble(point_gr2[j + 1]) - left_limit_y) + k2 * (Convert.ToDouble(point_gr2[point_gr2.Length - 2]) - left_limit_y), right_limit_x - Convert.ToDouble(point_gr1[point_gr1.Length - 3]), k2 * (right_limit_y - Convert.ToDouble(point_gr2[point_gr2.Length - 2])) + k1 * cord + k0 * (Convert.ToDouble(point_gr2[1]) - left_limit_y), color[count], 1);
        }

        public int Namegraph(string gr_table_name)
        {
            switch (gr_table_name)
            {
                case "ОМ": return 1;
                case "М": return 2;
                case "С": return 3;
                case "Б": return 4;
                case "ОБ": return 5;
                default: return 0;
            }
        }
        public string _Namegraph(string gr_table_name)
        {
            switch (gr_table_name)
            {
                case "1": return "ОМ";
                case "2": return "М";
                case "3": return "С";
                case "4": return "Б";
                case "5": return "ОБ";
                default: return "Р";
            }
        }

        public Color[] Name_area(int left_lim, int right_lim, int select_index, double l, double h)
        {
            var str_name = String.Empty;
            var shapes = new Main_figures(gr);
            int count = right_lim - left_lim - 1;
            Color[] color = new Color[count];
            count = 0;

            var gr_table2 = list_gr2[select_index];
            for (int i = left_lim + 1; i < right_lim; i++)
            {
                var gr_table1 = list_gr1[i];
                str_name = gr_table2.Name + gr_table1.Name;
                switch (Convert.ToInt32(str_name))
                {
                    case 55: case 45: case 54: color[count++] = Colors.DimGray; shapes.Text(l + (koef * h) / 10.5 * 12 - 20, 600, Name_Oblast_ST[4], Convert.ToInt32(0.5 * (koef * h) / 10.5)); shapes.Draw_Rect(l + (koef * h) / 10.5 * 12 - 65, 590, 40, 40, Colors.DimGray, 1); break;
                    case 44: case 53: color[count++] = Colors.Gray; shapes.Text(l + (koef * h) / 10.5 * 12 - 20, 550, Name_Oblast_ST[3], Convert.ToInt32(0.5 * (koef * h) / 10.5)); shapes.Draw_Rect(l + (koef * h) / 10.5 * 12 - 65, 540, 40, 40, Colors.Gray, 1); break;
                    case 43: case 52: case 51: color[count++] = Colors.DarkGray; shapes.Text(l + (koef * h) / 10.5 * 12 - 20, 500, Name_Oblast_ST[2], Convert.ToInt32(0.5 * (koef * h) / 10.5)); shapes.Draw_Rect(l + (koef * h) / 10.5 * 12 - 65, 490, 40, 40, Colors.DarkGray, 1); break;
                    case 42: case 34: case 35: color[count++] = Colors.Silver; shapes.Text(l + (koef * h) / 10.5 * 12 - 20, 450, Name_Oblast_ST[1], Convert.ToInt32(0.5 * (koef * h) / 10.5)); shapes.Draw_Rect(l + (koef * h) / 10.5 * 12 - 65, 440, 40, 40, Colors.Silver, 1); break;
                    default: color[count++] = Colors.Gainsboro; shapes.Text(l + (koef * h) / 10.5 * 12 - 20, 400, Name_Oblast_ST[0], Convert.ToInt32(0.5 * (koef * h) / 10.5)); shapes.Draw_Rect(l + (koef * h) / 10.5 * 12 - 65, 390, 40, 40, Colors.Gainsboro, 1); break;
                }
                str_name = str_name.First().ToString();
            }
            return color;
        }

        public string Namearea(string str_name)
        {
            switch (Convert.ToInt32(str_name))
            {
                case 55: case 45: case 54: return Name_Oblast_ST[4];
                case 44: case 53: case 4: return Name_Oblast_ST[3];
                case 43: case 52: case 51: case 3: return Name_Oblast_ST[2];
                case 42: case 34: case 35: return Name_Oblast_ST[1];
                default: return Name_Oblast_ST[0];
            }
        }

        public void Line_point_Area(List<Class_point> list_point1, List<Class_point> list_point2, Canvas gr)
        {
            var shapes = new Main_figures(gr);

            var p1_str = Function_Split(list_point1, 1);
            var p2_str = Function_Split(list_point2, 1);
            var list_criteria1 = Function_Split(list_point1, 0);
            var list_criteria2 = Function_Split(list_point2, 0);

            var obj = new Draw_main_object(gr);
            obj.Draw_main_line(list_criteria1, Colors.Black, true);
            obj.Draw_main_line(p1_str, Colors.Red, true);
            obj.Draw_main_line(list_criteria2, Colors.Black, false);
            obj.Draw_main_line(p2_str, Colors.Red, false);

            obj.Draw_main_point(p1_str, p2_str, Colors.Red);
            obj.Draw_main_point(list_criteria1, list_criteria2, Colors.Black);
        }

        public string Name_level_danger(string[] list_criteria, string[] p_str, List<Class_point> list_point, int index)
        {
            var point = 0.0;
            var name_gr = String.Empty;

            for (int i = index; i < list_criteria.Length - 1; i = i + 2)
            {
                point = Math.Abs((Convert.ToDouble(list_criteria[i]) - Convert.ToDouble(p_str[1])) / (Convert.ToDouble(p_str[3]) - Convert.ToDouble(list_criteria[i])));
            }

            if (point > 1)
                name_gr = list_point[0].Name;
            else
                name_gr = list_point[1].Name;

            return name_gr;
        }

        public void Rect_Area(List<Class_point> list_point1, List<Class_point> list_point2, int count_cord1, int count_cord2, Canvas gr)
        {
            var shapes = new Main_figures(gr);
            Color[] colors;
            var p1_str = Function_Split(list_point1, 1);
            var p2_str = Function_Split(list_point2, 1);
            var list_criteria1 = Function_Split(list_point1, 0);
            var list_criteria2 = Function_Split(list_point2, 0);
            
            for (int i = 0; i <= count_cord2; i++)
            {
                colors = Name_area(0, count_cord1, i + 1, gr.Width * 0.5, gr.Height * 0.55);
                if (i == 0)
                    Draw_main_rect(list_criteria1, list_criteria2, colors, 1, 0, 0, 0);
                else if (i == count_cord2)
                    Draw_main_rect(list_criteria1, list_criteria2, colors, 0, 0, 1, 0);
                else
                    Draw_main_rect(list_criteria1, list_criteria2, colors, 0, 1, 0, 2 * (i - 1));
            }
            
            var x = ((p1_str.Length - 1) / 2 < 4) ? Convert.ToDouble(p1_str[4]) : Convert.ToDouble(p1_str[2]);
            var y = ((p2_str.Length - 1) / 2 < 4) ? Convert.ToDouble(p2_str[5]) : Convert.ToDouble(p2_str[3]);

            var x1 = Convert.ToDouble(p1_str[0]);
            var x2 = Convert.ToDouble(p1_str[2]);
            var y1 = Convert.ToDouble(p2_str[1]);
            var y2 = Convert.ToDouble(p2_str[3]);

            if (x1 > x2 && y1 > y2) shapes.Draw_Rect(x2, y2, x - Convert.ToDouble(p1_str[0]), y - Convert.ToDouble(p2_str[1]), Colors.Red, 0.3);
            else if (x1 > x2 && y1 < y2) shapes.Draw_Rect(x2, y1, x - Convert.ToDouble(p1_str[0]), y - Convert.ToDouble(p2_str[1]), Colors.Red, 0.3);
            else if (x1 < x2 && y1 < y2) shapes.Draw_Rect(x1, y1, x - Convert.ToDouble(p1_str[0]), y - Convert.ToDouble(p2_str[1]), Colors.Red, 0.3);
            else if (x1 < x2 && y1 > y2) shapes.Draw_Rect(x1, y2, x - Convert.ToDouble(p1_str[0]), y - Convert.ToDouble(p2_str[1]), Colors.Red, 0.3);

            var name_gr1 = Name_level_danger(list_criteria1, p1_str, list_point1, 0);
            var name_gr2 = Name_level_danger(list_criteria2, p2_str, list_point2, 1);
            danger_level.Text = Namearea(name_gr2 + name_gr1);
        }

        public List<Class_point_gr> Convert_List(List<Class_point_gr> list, double l, double h, bool flag)
        {
            var count = 0;
            List<Class_point_gr> _list = new List<Class_point_gr>();
            double interval_cord_p = 0;
            double gran_left = 0;
            double gran_right = 0;
            var mass = new double[list.Count - 1];

            for (var index = 0; index < list.Count; index++)
            {
                var str = String.Empty;

                var _xy = list[index].xy.Split(';');

                for (int i = 0; i < _xy.Length - 1; i++)
                {
                    var xy = _xy[i].Split('/');
                    if (index == 0 && Convert.ToDouble(xy[0]) == 1)
                    {
                        interval_cord_p = Convert.ToDouble(xy[1]);
                    }
                    else if (Convert.ToDouble(xy[0]) == 1) mass[count++] = Convert.ToDouble(xy[1]);
                    if (flag)
                        str += (l + h * 0.76 * Convert.ToDouble(xy[1])).ToString() + ";" + (h * (0.9 - 0.76 * Convert.ToDouble(xy[0]))).ToString() + ";";
                    else str += (l - h * 0.76 * Convert.ToDouble(xy[0])).ToString() + ";" + (h * (1.1 + 0.76 * Convert.ToDouble(xy[1]))).ToString() + ";";
                }
                _list.Add(new Class_point_gr() { Name = list[index].Name, xy = str });
            }
            mass.ToList().Sort();
            for (int i = 0; i < mass.Length; i++)
            {
                if (interval_cord_p >= mass[i])
                {
                    gran_left = Convert.ToDouble(mass[i]);
                    if (i == mass.Length - 1)
                        gran_right = 1;
                }
                else
                {
                    gran_right = Convert.ToDouble(mass[i]);
                    break;
                }
            }
            if (flag)
                interval_p[0] = (l + h * 0.76 * gran_left).ToString() + ";" + (l + h * 0.76 * gran_right).ToString() + ";" + (h * 0.14).ToString();
            else interval_p[1] = (h * (1.1 + 0.76 * gran_left)).ToString() + ";" + (h * (1.1 + 0.76 * gran_right)).ToString() + ";" + (l - h * 0.76).ToString();
            return _list;
        }

        public void Graph_Build(List<Class_point_gr> list, bool flag, Canvas gr)
        {
            double x_graph_map = 0;
            double y_graph_map = 0;
            double interval = 0;

            var shapes = new Main_figures(gr);

            for (int index = 0; index < list.Count; index++)
            {
                int count = 0;

                var xy = list[index].xy.Split(';');
                //Створюємо масив для роботи з графіком
                var arr = new double[xy.Length - 1];

                for (int i = 0; i < xy.Length - 1; i = i + 2)
                {
                    if (flag)
                    {
                        x_graph_map = 0.7 * gr.Width / 2;
                        y_graph_map = 20;
                    }
                    else
                    {
                        x_graph_map = 10;
                        y_graph_map = 1.1 * gr.Height / 2;
                    }
                    //Наповнюємо масив відповідними координатами для відмалювання графіка
                    arr[count++] = Convert.ToDouble(xy[i]);
                    arr[count++] = Convert.ToDouble(xy[i + 1]);
                }

                Color color;
                double[] arr_leg;
                interval = interval + 20;

                //Вибір кольорів для графіків
                var gr_table = list[index] as Class_point_gr;
                switch (gr_table.Name)
                {
                    case "1": color = Colors.Blue; break;
                    case "2": color = Colors.Green; break;
                    case "3": color = Colors.Yellow; break;
                    case "4": color = Colors.Orange; break;
                    case "5": color = Colors.Red; break;
                    default: color = Colors.Navy; break;
                }
                arr_leg = new[] { x_graph_map, y_graph_map + interval, x_graph_map + koef * 60, y_graph_map + interval };
                //Відмальвуємо графік
                shapes.Draw_polyline(arr, 2, color, false);

                //Підписуємо легенду графіка
                shapes.Text(x_graph_map + koef * 60 + 10, y_graph_map + interval - 10, _Namegraph(gr_table.Name), Convert.ToInt32(0.25 * (koef * gr.Height) / 10.5));
                //Легенда графіка
                shapes.Draw_polyline(arr_leg, 3, color, false);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gr.Children.Clear();
            var h = gr.Height * 0.5;
            var l = gr.Width * 0.5;

            foreach (var item in list_gr1)
                item.Name = Namegraph(item.Name).ToString();
            foreach (var item in list_gr2)
                item.Name = Namegraph(item.Name).ToString();

            list_gr1 = list_gr1.OrderBy(u => u.Name).ToList();
            list_gr2 = list_gr2.OrderBy(u => u.Name).ToList();

            var shapes = new Main_figures(gr);
            var coordinate_axes = new Coordinate_axes(gr);

            coordinate_axes.Main_coordinate_axes();

            int count_cord1 = list_gr1.Count();
            int count_cord2 = 0;


            var _list_gr1 = Convert_List(list_gr1, l, h, true);
            var _list_gr2 = Convert_List(list_gr2, l * 0.9, h, false);

            Graph_Build(_list_gr1, true, gr);
            Graph_Build(_list_gr2, false, gr);

            var Crossing = new Сrossing();
            var list_point1 = Crossing.Intersection_gr_point(_list_gr1, interval_p[0], true);
            var list_point2 = Crossing.Intersection_gr_point(_list_gr2, interval_p[1], false);

            foreach (var item in list_point2)
            {
                if (item.Index == "0")
                    count_cord2++;
            }

            Rect_Area(list_point1, list_point2, count_cord1, count_cord2, gr);
            Line_point_Area(list_point1, list_point2, gr);

            gridXaml = XamlWriter.Save(gr);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            var document = new FixedDocument();
            var report_page = new FixedPage();
            report_page.Width = document.DocumentPaginator.PageSize.Width;
            report_page.Height = document.DocumentPaginator.PageSize.Height;
            report_page.Margin = new Thickness(20, 50, 20, 50);

            //Створення та запис заголовку.
            var title_text = new TextBlock();
            title_text.Text = "Звіт";
            title_text.FontSize = 30;
            title_text.Margin = new Thickness(document.DocumentPaginator.PageSize.Width * 0.45, 0, 0, 50);
            report_page.Children.Add(title_text);

            //Створення та запис графіків.
            StringReader stringReader = new StringReader(gridXaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            Canvas newGrid = (Canvas)XamlReader.Load(xmlReader);
            report_page.Children.Add(newGrid);

            //Створення та запис правила.
            var main_text = new TextBlock();
            main_text.Text = "Поточний рівень загрози: " + danger_level.Text;
            main_text.FontSize = 20;
            main_text.Margin = new Thickness(20, 700, 0, 0);
            report_page.Children.Add(main_text);

            var report_page_сontent = new PageContent();
            ((IAddChild)report_page_сontent).AddChild(report_page);
            document.Pages.Add(report_page_сontent);

            using (MemoryStream xpsStream = new MemoryStream())
            {
                using (Package package = Package.Open(xpsStream, FileMode.Create, FileAccess.ReadWrite))
                {
                    string packageUriString = "memorystream://file_report.xps";
                    Uri packageUri = new Uri(packageUriString);

                    PackageStore.AddPackage(packageUri, package);

                    XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Maximum, packageUriString);
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                    writer.Write(document);

                    FixedDocumentSequence document_report = xpsDocument.GetFixedDocumentSequence();
                    xpsDocument.Close();

                    PrintPreviewWindow printPreviewWnd = new PrintPreviewWindow(document_report);
                    printPreviewWnd.Owner = Owner;
                    printPreviewWnd.ShowDialog();
                    PackageStore.RemovePackage(packageUri);
                }
            }
        }

        private void Window_wpf_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
