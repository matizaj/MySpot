using System;
namespace MySpot.Core.ValueObjects
{
	public record Password
	{
		public string Value { get; set; }
		public Password(string value)
		{
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidDataException("Password cant be empty");
            }
            Value = value;
        }

        public static implicit operator Password(string value) => new(value);
        public static implicit operator string(Password password) => password.Value;
    }
}

