using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_program__module_sec
{
    /// <summary>
    /// Логика взаимодействия для ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        Class_table gr_table;
        MainWindow window = new MainWindow();
        int count_txtbox = 0;

        public ChildWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Class_Model.Buffer != null)
            {
                var id = Convert.ToInt32(Class_Model.Buffer);
                using (bd_table db = new bd_table())
                {
                    gr_table = db.table.FirstOrDefault(c => c.Id == id);
                }
            }

            if (gr_table != null)
            {
                this.Title = "Редагування даних графіка";
                count_txtbox = gr_table.xy.Split(';').Length- 1;
                numericUpDown.Value = count_txtbox;
                Name_box.Text = gr_table.Name;
                type_gr.Text = gr_table.index;
                Edit_data(gr_table.xy);
            }
        }

        private void Add_TextBox(int left_lim, int right_lim)
        {
            TextBox txtBox;
            int coordinate_x = 0;
            int coordinate_y = 0;
            for (int i = left_lim; i <= right_lim; i++)
            {
                txtBox = new TextBox() { Name = "txtBox" + i.ToString(), Height = 22, Width = 60 };
                if (i % 2 != 0)
                {
                    coordinate_y = -40;
                    coordinate_x = 10;
                }
                else
                {
                    coordinate_y = 40;
                    coordinate_x = -60;
                }

                txtBox.Margin = new Thickness(coordinate_x, coordinate_y, 0, 0);
                txtBox.Height = 25;
                txtBox.FontSize = 16;
                Canvas_Panel.Children.Add(txtBox);
                Canvas_Panel.RegisterName(txtBox.Name, txtBox);
            }
        }

        private void Add_data(int count)
        {
            gr_table.Name = Name_box.Text;
            gr_table.index = type_gr.Text;

            for (int i = 1; i <= 2 * count; i++)
            {
                object item = Canvas_Panel.FindName("txtBox" + i.ToString());
                TextBox text = (TextBox)item;
                if(i%2==0)
                    gr_table.xy += text.Text + ";";
                else gr_table.xy += text.Text + "/";
            }
        }

        private void Edit_data(string text)
        {
            string[] number = text.Split(';');

            Add_TextBox(1, 2 * (number.Length - 1));

            for (int i = 0; i <= 2 * (number.Length - 1) - 2; i = i + 2)
            {
                string[] xy = number[i - i/2].Split('/');
                for (int j = 1; j <= xy.Length; j++)
                {
                    object item = Canvas_Panel.FindName("txtBox" + (i + j).ToString());
                    TextBox edit_text = (TextBox)item;

                    edit_text.Text = xy[j - 1];
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var value = Convert.ToInt32(numericUpDown.Value);
            if (count_txtbox > value)
            {
                for (int i = 2 * Convert.ToInt32(numericUpDown.Value) + 1; i <= 2 * count_txtbox; i++)
                {
                    object item = Canvas_Panel.FindName("txtBox" + i.ToString());
                    TextBox text = (TextBox)item;
                    Canvas_Panel.Children.Remove(text);
                    Canvas_Panel.UnregisterName(text.Name);
                }
            }
            else if (count_txtbox < value)
            {
                Add_TextBox(2 * count_txtbox + 1, 2 * value);
            }
            count_txtbox = value;

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Name_box.Text == "" || type_gr.Text == "")
            {
                MessageBox.Show("Перевірте поля, деякі поля є пустими.");
            }
            else
            {
                using (bd_table db = new bd_table())
                {
                    if (gr_table != null)
                    {
                        gr_table.xy = String.Empty;
                        Add_data(Convert.ToInt32(numericUpDown.Value));
                        db.table.Update(gr_table);
                    }
                    else
                    {
                        gr_table = new Class_table();
                        Add_data(Convert.ToInt32(numericUpDown.Value));
                        db.table.Add(gr_table);
                    }
                    db.SaveChanges();
                }
                window.Activate();
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            window.Activate();
            this.Close();
        }

        private void numericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > 2)
            {
                Add.IsEnabled = true;
            }
            else Add.IsEnabled = false;
        }
    }
}
