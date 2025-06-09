using APBD_Test_Retake.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Test_APBD_2.Services;

namespace Test_APBD_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RacersController : ControllerBase
{
    private readonly IDbService _service;

    public RacersController(IDbService service)
    {
        _service = service;
    }

    [HttpGet("{idRacer}/participations")]
    public async Task<IActionResult> GetRacerParticipants(int idRacer)
    {
        try
        {
            var result = await _service.GetRacerInfo(idRacer);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Internal server error occured in getting client info.");
            Console.WriteLine(ex.Message);
            return StatusCode(500);
        }
    }


}