using MySpot.Application.Abstractions;

namespace MySpot.Application.Commands
{
	// record behind is a normal class but props has {get; set init;}
	// it means valiue can be initialezed ONLY when it will be created
	public record ReserveParkingSpotForVehicle(Guid ParkingSpotId,
		Guid ReservationId,
		string EmployeeName,
		string LicensePlate,
		DateTime Date,
		int Capacity): ICommand;
}

