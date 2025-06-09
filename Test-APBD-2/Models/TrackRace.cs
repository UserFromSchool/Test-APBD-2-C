using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_APBD_2.Models;

public class TrackRace
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Track))]
    public int TrackId { get; set; }
    
    [ForeignKey(nameof(Race))]
    public int RaceId { get; set; }
    
    public int Laps { get; set; }
    
    public int? BestTimeInSeconds { get; set; }
    
    public Track Track { get; set; }
    
    public Race Race { get; set; }
    
    public List<RaceParticipation> RaceParticipations { get; set; }
}