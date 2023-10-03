using System;
namespace MySpot.Core.ValueObjects
{
	public record Email
	{
		public string Value { get; set; }

		public Email(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new InvalidDataException("Email cant be empty");
			}
			Value = value;
		}

		public static implicit operator Email(string data) => new Email(data);
		public static implicit operator string(Email email) => email.Value;
	}
}

