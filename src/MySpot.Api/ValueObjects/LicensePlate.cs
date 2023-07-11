using System;
using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
	// record implicitly implement IEquetable<T>, no need to implement method Equals()
	public record LicensePlate
	{
		public string Value { get; }

		public LicensePlate(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new EmptyLicensePlateException();		

            }
			if (value.Length < 3 || value.Length> 8)
			{
				throw new InvalidLicensePlateException(value);

            }

			Value = value;
		}

		// static polimorphism
		public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;

		public static implicit operator LicensePlate(string licensePlate) => new LicensePlate(licensePlate);
	}

}

