namespace Test_APBD_2.DTOs;

public class RacerRaceParticipationsInfoDTO
{
    public int RacerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<RaceParticipationInfoDTO> Participations { get; set; }
}