﻿using Cookbook.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cookbook.Data
{
    /// <summary>
    /// Context to connect to the database.
    /// </summary>
    public class CookbookContext:DbContext
    {
        public CookbookContext()
        {

        }

        public CookbookContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<MealType> MealTypes { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeTag> RecipesTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Maps the relations between the tables.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeTag>().HasKey(rt=>new {rt.RecipeId,rt.TagId });
            modelBuilder.Entity<MealType>().Property(mt => mt.Name).HasMaxLength(32);
            modelBuilder.Entity<Recipe>().Property(r => r.Name).HasMaxLength(64);
            modelBuilder.Entity<Recipe>().Property(r => r.Ingredients).HasMaxLength(256);
            modelBuilder.Entity<Recipe>().Property(r => r.Preparation).HasMaxLength(2048);
            modelBuilder.Entity<Tag>().Property(t => t.Name).HasMaxLength(32);
            modelBuilder.Entity<RecipeTag>().HasOne(rt => rt.Recipe).WithMany(r => r.RecipeTags).HasForeignKey(rt => rt.RecipeId);
            modelBuilder.Entity<RecipeTag>().HasOne(rt => rt.Tag).WithMany(t => t.RecipeTags).HasForeignKey(rt => rt.TagId);
            modelBuilder.Entity<Recipe>().HasOne(r => r.MealType).WithMany(m => m.Recipes).HasForeignKey(r => r.MealTypeId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
