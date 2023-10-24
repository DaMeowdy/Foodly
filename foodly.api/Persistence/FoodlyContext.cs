using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using foodly.api.Domain;
using Microsoft.Extensions.Configuration;
namespace foodly.api.Persistence;

public partial class FoodlyContext : DbContext
{
    private readonly IConfigurationRoot _configRoot;
    public FoodlyContext()
    {
        _configRoot = new ConfigurationBuilder().AddUserSecrets<FoodlyContext>().Build();
    }

    public FoodlyContext(DbContextOptions<FoodlyContext> options)
        : base(options)
    {
        _configRoot = new ConfigurationBuilder().AddUserSecrets<FoodlyContext>().Build();
    }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<Voter> Voters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configRoot["connection_string"]);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("timescaledb");

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("food_pkey");

            entity.ToTable("food");

            entity.Property(e => e.FoodId)
                .ValueGeneratedNever()
                .HasColumnName("food_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.FoodDietaryRequirements)
                .HasColumnType("json")
                .HasColumnName("food_dietary_requirements");
            entity.Property(e => e.FoodImageUrl).HasColumnName("food_image_url");
            entity.Property(e => e.FoodName).HasColumnName("food_name");
            entity.Property(e => e.FoodSlug).HasColumnName("food_slug");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.VotesId).HasName("votes_pkey");

            entity.ToTable("votes");

            entity.Property(e => e.VotesId)
                .ValueGeneratedNever()
                .HasColumnName("votes_id");
            entity.Property(e => e.VoterId).HasColumnName("voter_id");
            entity.Property(e => e.Votes)
                .HasColumnType("json")
                .HasColumnName("votes");

            entity.HasOne(d => d.Voter).WithMany(p => p.Votes)
                .HasForeignKey(d => d.VoterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("votes_voter_id_fkey");
        });

        modelBuilder.Entity<Voter>(entity =>
        {
            entity.HasKey(e => e.VoterId).HasName("voter_pkey");

            entity.ToTable("voter");

            entity.Property(e => e.VoterId)
                .ValueGeneratedNever()
                .HasColumnName("voter_id");
            entity.Property(e => e.DiscordID);
            entity.Property(e => e.TimeVoted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time_voted");
        });
        modelBuilder.HasSequence("chunk_constraint_name", "_timescaledb_catalog");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
