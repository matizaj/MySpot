using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Core.Entities;
using MySpot.Application.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReservationsController: ControllerBase 
    {
        private readonly IReservationsService _service;

        //controller cant hold any state, every request start from new state so static keyword need to be added
        public ReservationsController(IReservationsService service)
        {
            _service = service;
        }


        [HttpPost("vehicle")]
        public async Task<IActionResult> Post(ReserveParkingSpotForVehicle command)
        {
           var cmd = command with { ReservationId = Guid.NewGuid() };
           var id = await _service.ReserveForVehicleAsync(cmd);
            if(id == null)
            {
                return BadRequest();
            }
           return CreatedAtAction(nameof(Get), new {id = cmd.ReservationId}, cmd);
        }

        [HttpPost("cleaning")]
        public async Task<IActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _service.ReserveForCleaningAsync(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> Get() => Ok(await _service.GetAllWeeklyAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Reservation>> Get(Guid id)
        {
            var reservation = await _service.GetAsync(id);
            if(reservation == null)
            {
                return NotFound();
            }
          
            return Ok(reservation);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ChangeReservationLicensePlate command)
        {
            if(await _service.ChangeReservationLicensePlateAsync(command with { ReservationId = id}))
            {
                return NoContent();
            }
            return NotFound();
            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           if (await _service.DeleteAsync(new DeleteReservation(id)))
           {
                return NoContent();
           }
           return NotFound();
        }
    }
}
