namespace MySpot.Core.ValueObjects
{
	public record ParkingSpotId(Guid Value)
	{
		public Guid Value { get; } = Value;

        public static ParkingSpotId Create() => new(Guid.NewGuid());

        public static implicit operator Guid(ParkingSpotId date)
            => date.Value;

        public static implicit operator ParkingSpotId(Guid value)
            => new(value);

        public override string ToString() => Value.ToString("N");

    }
}

