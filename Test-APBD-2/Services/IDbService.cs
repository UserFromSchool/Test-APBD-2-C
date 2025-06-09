using Test_APBD_2.DTOs;

namespace Test_APBD_2.Services;

public interface IDbService
{

    public Task<RacerRaceParticipationsInfoDTO> GetRacerInfo(int idRacer);

    public Task AddNewParticipation(NewRaceParticipationInfoDTO request);
    
}