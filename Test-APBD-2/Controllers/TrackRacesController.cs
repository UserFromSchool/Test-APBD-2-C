using APBD_Test_Retake.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Test_APBD_2.DTOs;
using Test_APBD_2.Services;

namespace Test_APBD_2.Controllers;

[Route("api/track-races")]  // Otherwise would be TrackRaces
[ApiController]
public class TrackRacesController : ControllerBase
{
    private readonly IDbService _service;

    public TrackRacesController(IDbService service)
    {
        _service = service;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> AddParticipation([FromBody] NewRaceParticipationInfoDTO request)
    {
        try
        {
            await _service.AddNewParticipation(request);
            return Created();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Internal server error occured while adding new participation.");
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }
    
}