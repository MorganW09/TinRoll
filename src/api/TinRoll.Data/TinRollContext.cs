﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinRoll.Data.Entities;

namespace TinRoll.Data
{
    public class TinRollContext : DbContext
    {
        public TinRollContext(DbContextOptions<TinRollContext> options) : base(options)
        { }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var added = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Added).ToList();

            added.ForEach(a =>
            {
                a.Property(p => p.CreatedDate).CurrentValue = DateTime.UtcNow;
                a.Property(p => p.CreatedDate).IsModified = true;
                a.Property(p => p.UpdatedDate).CurrentValue = DateTime.UtcNow;
                a.Property(p => p.UpdatedDate).IsModified = true;
            });

            var updated = ChangeTracker.Entries<BaseEntity>().Where(E => E.State == EntityState.Modified).ToList();

            updated.ForEach(u =>
            {
                u.Property(p => p.UpdatedDate).CurrentValue = DateTime.UtcNow;
                u.Property(p => p.UpdatedDate).IsModified = true;
            });

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionTag>()
                .HasKey(qt => new {
                    qt.QuestionId,
                    qt.TagId
                });

            modelBuilder.Entity<QuestionTag>()
                .HasOne(qt => qt.Tag)
                .WithMany(qt => qt.QuestionTags)
                .HasForeignKey(qt => qt.TagId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionTag>()
                .HasOne(qt => qt.User)
                .WithMany(qt => qt.QuestionTags)
                .HasForeignKey(qt => qt.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(c => c.QuestionTags)
                .WithOne(e => e.User);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
    }
}
