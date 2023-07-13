using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReservationsController: ControllerBase 
    {
        private static Clock _clock = new();
        //controller cant hold any state, every request start from new state so static keyword need to be added
        private static  readonly ReservationsService _service = new ReservationsService(new()
        {
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000001")), new Week(_clock.Current()),"P1"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000002")), new Week(_clock.Current()),"P2"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000003")), new Week(_clock.Current()),"P3"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000004")), new Week(_clock.Current()),"P4"),
            new WeeklyParkingSpot(new ParkingSpotId(Guid.Parse("00000000-0000-0000-0000-000000000005")), new Week(_clock.Current()),"P5"),
        });
        

        [HttpPost]
        public IActionResult Post(CreateReservation command)
        {
           var cmd = command with { ReservationId = Guid.NewGuid() };
           var id = _service.Create(cmd);
            if(id == null)
            {
                return BadRequest();
            }
           return CreatedAtAction(nameof(Get), new {id = cmd.ReservationId}, cmd);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(_service.GetAllWeekly());

        [HttpGet("{id:guid}")]
        public ActionResult<Reservation> Get(Guid id)
        {
            var reservation = _service.Get(id);
            if(reservation == null)
            {
                return NotFound();
            }
          
            return Ok(reservation);
        }

        [HttpPut("{id:guid}")]
        public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
        {
            if(_service.Update(command with { ReservationId = id}))
            {
                return NoContent();
            }
            return NotFound();
            
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
           if (_service.Delete(new DeleteReservation(id)))
           {
                return NoContent();
           }
           return NotFound();
        }
    }
}
