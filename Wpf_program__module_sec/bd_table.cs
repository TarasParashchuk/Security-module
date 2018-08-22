
using Microsoft.EntityFrameworkCore;

namespace Wpf_program__module_sec
{
    class bd_table : DbContext
    {
        public DbSet<Class_table> table { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=graph.db");
        }
    }

}
