﻿using System;
namespace MySpot.Infrastructure.Auth
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SigninKey { get; set; }
		public TimeSpan? Expity { get; set; }
	}
}

