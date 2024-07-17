using Microsoft.EntityFrameworkCore;
using System;
using MushroomPocket;

public class MushroomContext : DbContext
{
    public DbSet<Character> Characters { get; set; }

    public string DbPath { get; }

    public MushroomContext()
    {
        DbPath = "mushroom.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasDiscriminator<string>("CharacterType")
            .HasValue<Waluigi>("Waluigi")
            .HasValue<Daisy>("Daisy")
            .HasValue<Wario>("Wario")
            .HasValue<Luigi>("Luigi")
            .HasValue<Peach>("Peach")
            .HasValue<Mario>("Mario");
    }
}