namespace Test_APBD_2.DTOs;

public class RaceParticipationInfoDTO
{
    public int Laps { get; set; }
    public int FinishTimeInSeconds { get; set; }
    public int Position { get; set; }
    public TrackInfoDTO Track { get; set; }
    public RaceInfoDTO Race { get; set; }
}