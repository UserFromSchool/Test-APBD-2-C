using System.ComponentModel.DataAnnotations;

namespace Test_APBD_2.Models;

public class Race
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }
    
    [StringLength(100)]
    public string Location { get; set; }
    
    public DateTime Date { get; set; }
    
    public List<TrackRace> TrackRaces { get; set; }
}