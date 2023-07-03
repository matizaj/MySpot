using Microsoft.AspNetCore.Mvc;
using MySpot.Api.NewFolder;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReservationsController: ControllerBase 
    {
        //controller can hold any state, every request start from new state so static keyword need to be added
        private readonly ReservationsService _service = new ReservationsService();

        [HttpPost]
        public IActionResult Post(Reservation reservation)
        {
           var id = _service.Create(reservation);
            if(id == null)
            {
                return BadRequest();
            }
           return CreatedAtAction(nameof(Get), new {id = reservation.Id}, reservation);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(_service.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = _service.Get(id);
            if(reservation == null)
            {
                return NotFound();
            }
          
            return Ok(reservation);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            if(_service.Update(id, reservation))
            {
                return NoContent();
            }
            return NotFound();
            
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
           if (_service.Delete(id))
           {
                return NoContent();
           }
           return NotFound();
        }
    }
}
