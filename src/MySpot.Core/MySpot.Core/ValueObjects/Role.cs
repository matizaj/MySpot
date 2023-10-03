using System;
namespace MySpot.Core.ValueObjects
{
	public record Role
	{
        public string Value { get; set; }
        public Role(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidDataException("Role cant be empty");
            }
            Value = value;
        }

        public static implicit operator Role(string value) => new(value);
        public static implicit operator string(Role role) => role.Value;
    }
}

