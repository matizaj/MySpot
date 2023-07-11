using System;
namespace MySpot.Api.ValueObjects
{
	public record ParkingSpotName(string Value)
	{
		public string Value { get; } = Value;

		public static implicit operator string(ParkingSpotName spotName) => spotName.Value;
		public static implicit operator ParkingSpotName(string spotName) => new ParkingSpotName(spotName);
	}
}

