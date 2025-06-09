using Microsoft.EntityFrameworkCore;
using Test_APBD_2.Models;

namespace Test_APBD_2.Data;

public class DatabaseContext : DbContext
{
    
    public DbSet<Race> Races { get; set; }
    public DbSet<Racer> Racers { get; set; }
    public DbSet<RaceParticipation> RaceParticipations { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }
    
    public DatabaseContext(DbContextOptions options) : base(options) { }
    public DatabaseContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Racer>().HasData(new List<Racer>
        {
            new Racer { Id = 1, FirstName = "John", LastName = "Elvis" },
            new Racer { Id = 2, FirstName = "Lewis", LastName = "Hamilton" },
            new Racer { Id = 3, FirstName = "Max", LastName = "Verstappen" },
            new Racer { Id = 4, FirstName = "Mister", LastName = "NoParticipation" }
        });
        
        modelBuilder.Entity<Race>().HasData(new List<Race>
        {
            new Race { Id = 1, Name = "British Cup", Location = "Silverstone, 9", Date = new DateTime(2025, 6, 10) },
            new Race { Id = 2, Name = "Spanish Cup", Location = "Madrit, 4", Date = new DateTime(2025, 4, 10)}
        });
        
        modelBuilder.Entity<Track>().HasData(new List<Track>
        {
            new Track { Id = 1, Name = "Silverstone Circuit", LengthInKm = 2.9m  },
            new Track { Id = 2, Name = "Spanish Circuit", LengthInKm = 5.3m }
        });
        
        modelBuilder.Entity<TrackRace>().HasData(new List<TrackRace>
        {
            new TrackRace { Id = 1, RaceId = 1, TrackId = 1, Laps = 50, BestTimeInSeconds = 6000 },
            new TrackRace { Id = 2, RaceId = 2, TrackId = 1, Laps = 100, BestTimeInSeconds = 4567 }
        });
        
        modelBuilder.Entity<RaceParticipation>().HasData(new List<RaceParticipation>
        {
            new RaceParticipation { RacerId = 1, TrackRaceId = 1, FinishTimeInSeconds = 7000, Position = 1 },
            new RaceParticipation { RacerId = 2, TrackRaceId = 1, FinishTimeInSeconds = 6500, Position = 2 },
            new RaceParticipation { RacerId = 3, TrackRaceId = 1, FinishTimeInSeconds = 6400, Position = 3 },
            new RaceParticipation { RacerId = 1, TrackRaceId = 2, FinishTimeInSeconds = 8900, Position = 1 },
            new RaceParticipation { RacerId = 2, TrackRaceId = 2, FinishTimeInSeconds = 7770, Position = 2 },
            new RaceParticipation { RacerId = 3, TrackRaceId = 2, FinishTimeInSeconds = 8000, Position = 3 }
        });
    }
    
}