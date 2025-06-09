namespace Test_APBD_2.DTOs;

public class NewRaceParticipationInfoDTO
{
    public string RaceName { get; set; }
    public string TrackName { get; set; }
    public List<ParticipationSpecsDTO> Participations { get; set; }
}