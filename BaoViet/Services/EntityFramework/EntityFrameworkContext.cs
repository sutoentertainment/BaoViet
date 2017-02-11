using BaoVietCore.Interfaces;
using BaoVietCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;

namespace BaoViet.Services.EntityFramework
{
    class EntityFrameworkContext : DbContext, IDatabase
    {
        string databaseName = "database.sqlite";

        public DbSet<FeedItem> Feeds { get; set; }
        public DbSet<CustomPaper> Papers { get; set; }

        public EntityFrameworkContext()
        {

        }

        public EntityFrameworkContext(string databaseName)
        {
            this.databaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=" + databaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomPaper>().Ignore(p => p.Margin);
            modelBuilder.Entity<CustomPaper>().Ignore(p => p.Parser);
            modelBuilder.Entity<PaperBase>().Ignore(p => p.Margin);
            modelBuilder.Entity<PaperBase>().Ignore(p => p.Parser);
            modelBuilder.Entity<PaperBase>().Ignore(p => p.PinCommand);
            modelBuilder.Entity<CustomPaper>().Ignore(p => p.PinCommand);
            base.OnModelCreating(modelBuilder);
        }

        public void Configurate()
        {
            this.Database.Migrate();
        }

        public IEnumerable<CustomPaper> GetCustomPaper()
        {
            using (var context = new EntityFrameworkContext())
            {
                return context.Papers.ToList();
            }
        }

        public IEnumerable<FeedItem> GetFeedItem()
        {
            using (var context = new EntityFrameworkContext())
            {
                return context.Feeds.ToList();
            }
        }

        public void AddFeedItem(FeedItem model)
        {
            using (var context = new EntityFrameworkContext())
            {
                context.Feeds.Add(model);
                context.SaveChanges();
            }
        }

        public void DeleteFeed(FeedItem feed)
        {
            using (var context = new EntityFrameworkContext())
            {
                context.Feeds.Remove(feed);
                context.SaveChanges();
            }
        }

        public void DeletePaper(CustomPaper p)
        {
            using (var context = new EntityFrameworkContext())
            {
                context.Papers.Remove(p);
                context.SaveChanges();
            }
        }

        public void AddPaper(CustomPaper p)
        {
            using (var context = new EntityFrameworkContext())
            {
                context.Papers.Add(p);
                context.SaveChanges();
            }
        }
    }
}
