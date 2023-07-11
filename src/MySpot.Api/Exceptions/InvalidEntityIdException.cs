﻿using System;
namespace MySpot.Api.Exceptions
{
	public class InvalidEntityIdException:CustomException
	{
		public InvalidEntityIdException(Guid id):base($"Invalid reservation id {id}")
		{
		}
	}
}

