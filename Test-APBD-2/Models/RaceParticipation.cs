using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test_APBD_2.Models;

[PrimaryKey(nameof(TrackRaceId), nameof(RacerId))]
public class RaceParticipation
{
    [ForeignKey(nameof(TrackRace))]
    public int TrackRaceId { get; set; }
    
    [ForeignKey(nameof(Race))]
    public int RacerId { get; set; }
    
    public int FinishTimeInSeconds { get; set; }
    
    public int Position { get; set; }
    
    public TrackRace TrackRace { get; set; }
    
    public Racer Racer { get; set; }
}