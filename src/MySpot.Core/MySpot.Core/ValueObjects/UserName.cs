using System;
namespace MySpot.Core.ValueObjects
{
	public record UserName
	{
		public string Value { get; set; }
		public UserName(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new InvalidDataException("User cant be empty");
			}
			Value = value;
		}
		public static implicit operator UserName(string value) => new(value);
		public static implicit operator string(UserName userName) => userName.Value;
	}
}

