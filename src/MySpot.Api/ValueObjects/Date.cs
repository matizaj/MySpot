using System;
namespace MySpot.Api.ValueObjects
{
	public record Date
	{
		public DateTimeOffset Value { get; }

		public Date(DateTimeOffset value)
		{
			Value = value.Date;
		}

		public Date AddDays(int days) => new Date(Value.AddDays(days));

		public static implicit operator DateTimeOffset(Date date) => date.Value;

		public static implicit operator Date(DateTimeOffset date) => new Date(date);

		public static bool operator < (Date d1, Date d2) => d1.Value < d2.Value;

        public static bool operator >(Date d1, Date d2) => d1.Value > d2.Value;

		public static Date Now() => new Date(DateTimeOffset.Now);	
    }
}

