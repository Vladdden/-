using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkForm
{
    class DataContext : DbContext
    {
        public DataContext()
                    : base("ConnectionToWorkerForm")
        { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Subdivision> Subdivisions  { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
