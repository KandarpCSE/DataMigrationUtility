using Dmu_Console.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmu_Console.Data
{
    public class CommonContext : DbContext
    {
        public DbSet<SourceModel> Source { get; set; }
        public DbSet<DestinationModel> Destination { get; set; }
        public DbSet<MigrationStatus> MigrationStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Kandarp Patel\\Documents\\Test2DB.mdf\";Integrated Security=True;Connect Timeout=30");
        }
    }
}
