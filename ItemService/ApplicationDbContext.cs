﻿using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Schedule> ScheduleItems { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationItems> ApplicationsItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
