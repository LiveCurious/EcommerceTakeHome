using System;
using EcommerceTakeHome.Core.Domain;
using EcommerceTakeHome.Core.Domain.ContactDetail;
using EcommerceTakeHome.Core.Domain.DeliveryAppointment;
using EcommerceTakeHome.Core.Domain.PaymentDetail;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTakeHome.DataAccess
{
    public class MyDbContext : DbContext
        {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStep> OrderSteps { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<DeliveryAppointment> DeliveryAppointments { get; set; }
        public DbSet<PaymentDetail> Payment { get; set; }

        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Ecommerce.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(x => x.Steps)
                .WithOne(x=>x.Order);
            modelBuilder.Entity<Order>()
                .OwnsOne(x => x.PaymentDetail);
            modelBuilder.Entity<Order>()
                .OwnsOne(x => x.ContactDetail);
            modelBuilder.Entity<Order>()
                .OwnsOne(x => x.DeliveryAppointment);

        }
    }
}
