using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Wpf_program__module_sec
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var db = new bd_table())
            {
                db.Database.Migrate();
            }
        }
    }
}
