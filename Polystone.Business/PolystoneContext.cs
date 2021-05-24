using Microsoft.EntityFrameworkCore;
using Polystone.Business.Models;
using System;
using System.IO;

namespace Polystone.Business
{
    public class PolystoneContext : DbContext
    {
        public readonly string SqliteFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Polystone");

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!Directory.Exists(SqliteFolder))
            {
                Directory.CreateDirectory(SqliteFolder);
            }
            optionsBuilder.UseSqlite($"Data Source={SqliteFolder}/Polystone.db");
        }
    }
}
