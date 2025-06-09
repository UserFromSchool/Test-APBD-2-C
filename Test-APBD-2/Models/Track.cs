using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Test_APBD_2.Models;

public class Track
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; }
    
    [Precision(5,2)]
    public decimal LengthInKm { get; set; }
    
    public List<TrackRace> TrackRaces { get; set; }
}