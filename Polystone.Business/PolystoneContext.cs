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
        public DbSet<AccountHistory> AccountHistories { get; set; }
        public DbSet<AccountCatch> AccountCatches { get; set; }
        public DbSet<AccountCandy> AccountCandies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!Directory.Exists(SqliteFolder))
            {
                Directory.CreateDirectory(SqliteFolder);
            }
            optionsBuilder.UseSqlite($"Data Source={SqliteFolder}/Polystone.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<Account>().HasMany(a_ => a_.AccountHistories).WithOne(s_ => s_.Account).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Account>().HasMany(a_ => a_.AccountCatches).WithOne(l_ => l_.Account).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Account>().HasMany(a_ => a_.AccountCandies).WithOne(l_ => l_.Account).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
