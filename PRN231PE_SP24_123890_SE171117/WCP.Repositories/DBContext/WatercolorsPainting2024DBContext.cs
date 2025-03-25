﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WCP.Repositories.Models;

namespace WCP.Repositories.DBContext;

public partial class WatercolorsPainting2024DBContext : DbContext
{
    public WatercolorsPainting2024DBContext()
    {
    }

    public WatercolorsPainting2024DBContext(DbContextOptions<WatercolorsPainting2024DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Style> Styles { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<WatercolorsPainting> WatercolorsPaintings { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=WatercolorsPainting2024DB;User ID=sa;Password=12345");

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.StyleId).HasName("PK__Style__8AD14640751C8F77");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__UserAcco__DA6C70BA6FBD95A1");

            entity.Property(e => e.UserAccountId).ValueGeneratedNever();
        });

        modelBuilder.Entity<WatercolorsPainting>(entity =>
        {
            entity.HasKey(e => e.PaintingId).HasName("PK__Watercol__CF2D90F2934E12D2");

            entity.HasOne(d => d.Style).WithMany(p => p.WatercolorsPaintings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Watercolo__Style__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}