using APBD_Test_Retake.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Test_APBD_2.Data;
using Test_APBD_2.DTOs;
using Test_APBD_2.Models;

namespace Test_APBD_2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<RacerRaceParticipationsInfoDTO> GetRacerInfo(int idRacer)
    {
        // Join for all the required information
        var racer = await _context.Racers
            .Include(e => e.RaceParticipations)
            .ThenInclude(e => e.TrackRace)
            .ThenInclude(e => e.Track)
            .Include(e => e.RaceParticipations)
            .ThenInclude(e => e.TrackRace)
            .ThenInclude(e => e.Race)
            .Where(e => e.Id == idRacer)
            .FirstOrDefaultAsync();
        
        // Check if racer was found
        if (racer is null)
            throw new NotFoundException($"Couldn't find the racer with id {idRacer}.");
        
        // Return the information
        return new RacerRaceParticipationsInfoDTO
        {
            RacerId = racer.Id,
            FirstName = racer.FirstName,
            LastName = racer.LastName,
            Participations = racer.RaceParticipations.Select(e => new RaceParticipationInfoDTO
            {
                Position = e.Position,
                FinishTimeInSeconds = e.FinishTimeInSeconds,
                Laps = e.TrackRace.Laps,
                Race = new RaceInfoDTO
                {
                    Name = e.TrackRace.Race.Name,
                    Location = e.TrackRace.Race.Location,
                    Date = e.TrackRace.Race.Date
                },
                Track = new TrackInfoDTO
                {
                    Name = e.TrackRace.Track.Name,
                    LengthInKm = e.TrackRace.Track.LengthInKm
                }
            }).ToList()
        };
    }

    public async Task AddNewParticipation(NewRaceParticipationInfoDTO request)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Check if race exists
            var race = await _context.Races
                .Where(e => e.Name == request.RaceName)
                .FirstOrDefaultAsync();
            if (race is null)
                throw new NotFoundException($"Race with name {request.RaceName} is not found.");
            
            // Check if track exists
            var track = await _context.Tracks
                .Where(e => e.Name == request.TrackName)
                .FirstOrDefaultAsync();
            if (track is null)
                throw new NotFoundException($"Track with name {request.TrackName} is not found.");

            // Ensure request is proper (no positions and ids duplicates)
            var racersIds = request.Participations.Select(e => e.RacerId).ToList();
            if (racersIds.Count != racersIds.Distinct().Count())
                throw new BadRequestException("There are racers ids duplicates.");
            var positions = request.Participations.Select(e => e.Position).ToList();
            if (positions.Count != positions.Distinct().Count())
                throw new BadRequestException("There are positions duplicates.");
            if (positions.Any(e => e <= 0))
                throw new BadRequestException("Position must be greater than zero.");
            var times = request.Participations.Select(e => e.FinishTimeInSeconds).ToList();
            if (times.Any(e => e <= 0))
                throw new BadRequestException("There are finishing times below or equal to zero.");
            
            // Ensure all racers are found
            var racersFoundAmount = await _context.Racers.Where(e => racersIds.Contains(e.Id)).CountAsync();
            if (racersFoundAmount != racersIds.Count)
                throw new BadRequestException("Some of the racers are not present in the database.");
            
            // Find the proper TrackRace
            var trackRace = await _context.TrackRaces
                .Where(e => e.TrackId == track.Id && e.RaceId == race.Id)
                .FirstOrDefaultAsync();
            if (trackRace is null)
                throw new NotFoundException($"Race {request.RaceName} does not take place on track {request.TrackName}.");
            
            // Check if maybe racers have already participation there
            var foundParticipation = await _context.RaceParticipations
                .Where(e => e.TrackRaceId == trackRace.Id && racersIds.Contains(e.RacerId))
                .CountAsync();
            if (foundParticipation > 0)
                throw new BadRequestException("There are participations already present in the database.");
            
            // Insert new participation
            var participations = request.Participations
                .Select(e => new RaceParticipation
                {
                    RacerId = e.RacerId,
                    Position = e.Position,
                    FinishTimeInSeconds = e.FinishTimeInSeconds,
                    TrackRaceId = trackRace.Id
                })
                .ToList();
            await _context.RaceParticipations.AddRangeAsync(participations);
            await _context.SaveChangesAsync();
            
            // Handle new record
            var betterTime = -1;
            if (trackRace.BestTimeInSeconds is null)
            {
                betterTime = request.Participations
                    .Select(e => e.FinishTimeInSeconds)
                    .Max();
            }
            else
            {
                var foundBetterTimes = request.Participations
                    .Where(e => e.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
                    .ToList();
                if (foundBetterTimes.Count > 0)
                {
                    betterTime = foundBetterTimes
                        .Select(e => e.FinishTimeInSeconds)
                        .Max();
                }
            }
            
            // Update new record
            if (betterTime != -1)
            {
                trackRace.BestTimeInSeconds = betterTime;
                _context.Update(trackRace);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch (SqlException e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}