using CaffeBar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeBar
{
    public class ModelContext: DbContext
    {
        public ModelContext(): base("Data Source=DESKTOP-EITPB7M;Initial Catalog=CaffeBar;Integrated Security=True") { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsInOrder> ProductsInOrder { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ProductsInReservation> ProductsInReservation { get; set; }
        public DbSet<Table> Tables{ get; set; }
    }
}
