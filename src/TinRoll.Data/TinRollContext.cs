﻿using Microsoft.EntityFrameworkCore;
using TinRoll.Data.Entities;

namespace TinRoll.Data
{
    public class TinRollContext : DbContext
    {
        public TinRollContext(DbContextOptions<TinRollContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }

        //public Task<int> SaveChanges()
        //{
        //    ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList(); 
        //}
    }
}
