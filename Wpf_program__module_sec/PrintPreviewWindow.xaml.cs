using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf_program__module_sec
{
    /// <summary>
    /// Логика взаимодействия для PrintPreviewWindow.xaml
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        public PrintPreviewWindow(IDocumentPaginatorSource document)
        {
            InitializeComponent();
            DataContext = document;
        }
    }
}
