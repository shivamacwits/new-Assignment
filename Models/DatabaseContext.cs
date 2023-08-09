using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = string.Format("Server = (localdb)\\MSSQLLocalDB; Database = EmpRegistration; Trusted_Connection = True; MultipleActiveResultSets = true");
            options.UseSqlServer(connectionString);
        }
        public DbSet<State> State { get; set; }
        public DbSet<Employee> Employee { get; set; }

        /*#region StoreProcedures
        public DbSet<CostModelProductMappingData> CostModelProductMappingData { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostModelProductMappingData>()
                .Property(b => b.Price)
                .HasPrecision(18, 6);
           
        }*/


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<State>().HasNoKey();
        }
    }
}
