using System.ComponentModel.DataAnnotations;

namespace Test_APBD_2.Models;

public class Racer
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [StringLength(100)]
    public string LastName { get; set; }
    
    public List<RaceParticipation> RaceParticipations { get; set; }
    
}