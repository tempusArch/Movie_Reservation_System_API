using System.Net.NetworkInformation;
using System.Security.Cryptography;
using MovieReservationSystemAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace MovieReservationSystemAPI.Infrastructure;

public class MovieReservationSystemApiDbContext : DbContext {
    public MovieReservationSystemApiDbContext(DbContextOptions<MovieReservationSystemApiDbContext> options) : base(options) {

    }
    public DbSet<Genre> GenreTable {get; set;}
    public DbSet<Hall> HallTable {get; set;}
    public DbSet<Movie> MovieTable {get; set;}
    public DbSet<Reservation> ReservationTable {get; set;}
    public DbSet<ReservedSeat> ReservedSeatTable {get; set;}
    public DbSet<Seat> SeatTable {get; set;}
    public DbSet<Showtime> ShowtimeTable {get; set;}

    public DbSet<User> UserTable {get; set;}
    public DbSet<RefreshToken> RefreshTokenTable {get; set;}

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<decimal>()
            .HavePrecision(10, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Movie>().HasIndex(m => m.Title);
        modelBuilder.Entity<Movie>().HasIndex(m => m.Description);

        modelBuilder.Entity<ReservedSeat>().HasIndex(rs => new {rs.ShowtimeId, rs.SeatId}).IsUnique();

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.ShowtimeRisuto)
            .WithOne(st => st.Movie)
            .HasForeignKey(st => st.MovieId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.GenreRisuto)
            .WithMany(g => g.MovieRisuto)
            .UsingEntity<Dictionary<string, object>>(
                "MovieGenre",
                j => j
                    .HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey("GenreId")
                    .OnDelete(DeleteBehavior.Cascade),

                j => j
                    .HasOne<Movie>()
                    .WithMany()
                    .HasForeignKey("MovieId")
                    .OnDelete(DeleteBehavior.Cascade),

                j => {
                    j.HasKey("MovieId", "GenreId");
                    j.ToTable("MovieGenreTable");
                }
            );

        modelBuilder.Entity<Showtime>()
            .HasMany(st => st.ReservationRisuto)
            .WithOne(r => r.Showtime)
            .HasForeignKey(r => r.ShowtimeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>().Property(r => r.ReservationStatus).IsRequired();

        modelBuilder.Entity<Reservation>()
            .ToTable(t => t.HasCheckConstraint("CK_ReservationTable_ReservationStatus", "ReservationStatus IN (0, 1, 2)"));

        modelBuilder.Entity<Reservation>()
            .HasMany(r => r.ReservedSeatRisuto)
            .WithOne(rt => rt.Reservation)
            .HasForeignKey(rt => rt.ReservationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReservedSeat>()
            .HasOne(rs => rs.Seat)
            .WithMany()
            .HasForeignKey(rs => rs.SeatId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ReservationRisuto)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Hall>()
            .HasMany(h => h.SeatRisuto)
            .WithOne(s => s.Hall)
            .HasForeignKey(s => s.HallId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReservedSeat>().HasKey(rs => new {rs.SeatId, rs.ShowtimeId});
    }
}