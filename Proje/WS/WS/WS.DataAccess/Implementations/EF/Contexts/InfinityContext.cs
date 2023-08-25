using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Model.Entities;

namespace WS.DataAccess.Implementations.EF.Contex
{
    public class InfinityContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=.\SQLEXPRESS;database=Infinity;trusted_connection=true;");
        }
        public DbSet<Departman>? Departmans { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Expense>? Expenses { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Payment>? Payments { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DbSet<Supplier>? Suppliers { get; set; }
        public DbSet<AdminPanelUser>? AdminPanelUsers { get; set; }


    }
}
