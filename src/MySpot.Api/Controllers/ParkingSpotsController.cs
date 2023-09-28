using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.Dtos;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpotsController : ControllerBase
    {
        private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reservaParkingSpotForVehicleHandler;
        private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotFroCleaningHandler;
        private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
        private readonly ICommandHandler<ChangeReservationLicensePlate> _changeLicensePlateHandler;
        private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _getWeeklyParkingSpots;

        public ParkingSpotsController(ICommandHandler<ReserveParkingSpotForVehicle> reservaParkingSpotForVehicleHandler,
            ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotFroCleaningHandler,
            ICommandHandler<DeleteReservation> deleteReservationHandler,
            ICommandHandler<ChangeReservationLicensePlate> changeLicensePlateHandler,
            IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpots)
        {
            _reservaParkingSpotForVehicleHandler = reservaParkingSpotForVehicleHandler;
            _reserveParkingSpotFroCleaningHandler = reserveParkingSpotFroCleaningHandler;
            _deleteReservationHandler = deleteReservationHandler;
            _changeLicensePlateHandler = changeLicensePlateHandler;
            _getWeeklyParkingSpots = getWeeklyParkingSpots;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
        {
            return Ok(await _getWeeklyParkingSpots.HandleAsync(query));
        }

        [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
        public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
        {
            await _reservaParkingSpotForVehicleHandler.HandleAsync(command with
            {
                ParkingSpotId = parkingSpotId,
                ReservationId = Guid.NewGuid(),
            });
            return NoContent();
        }

        [HttpPost("reservations/cleaning")]
        public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
        {
            await _reserveParkingSpotFroCleaningHandler.HandleAsync(command);
            return NoContent();
        }

        [HttpDelete("reservations/{reservationId:guid}")]
        public async Task<IActionResult> Delete(Guid reservationId)
        {
            await _deleteReservationHandler.HandleAsync(new DeleteReservation(reservationId));
            return NoContent();
        }

        [HttpPut("reservations/{reservationId:guid}")]
        public async Task<IActionResult> Put(Guid reservationId, ChangeReservationLicensePlate command)
        {
            await _changeLicensePlateHandler.HandleAsync(command with 
            { 
                ReservationId = reservationId
            });
            return NoContent();
        }


    }
}
