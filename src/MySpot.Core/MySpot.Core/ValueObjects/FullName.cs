using System;
namespace MySpot.Core.ValueObjects
{
	public record FullName
	{
        public string Value { get; set; }
        public FullName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidDataException("Password cant be empty");
            }
            Value = value;
        }

        public static implicit operator FullName(string value) => new(value);
        public static implicit operator string(FullName fullname) => fullname.Value;
    }
}

