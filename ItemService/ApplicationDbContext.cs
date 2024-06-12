﻿using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}