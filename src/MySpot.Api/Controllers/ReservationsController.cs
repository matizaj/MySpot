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
