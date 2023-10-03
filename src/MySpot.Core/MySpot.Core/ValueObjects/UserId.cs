using System;
namespace MySpot.Core.ValueObjects
{
	public record UserId
	{
		public Guid Value { get; }

        public static UserId Create() => new(Guid.NewGuid());

        public UserId(Guid value)
        {
            
            Value = value;
        }

        public static implicit operator Guid(UserId userId) => userId.Value;
        public static implicit operator UserId(Guid guid) => new UserId(guid);
        public override string ToString()
        {
            return Value.ToString();
        }

    }
    
}

