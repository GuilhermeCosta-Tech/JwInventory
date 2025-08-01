﻿using Microsoft.EntityFrameworkCore;
using JwInventory.Domain.Entities;

namespace JwInventory.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseUser>()
                .HasDiscriminator<string>("UserType")
                .HasValue<AdminUser>("Admin")
                .HasValue<ManagerUser>("Gerente")
                .HasValue<EmployeeUser>("Colaborador");

            base.OnModelCreating(modelBuilder);
        }
    }
}
