﻿namespace MySpot.Application.Dtos
{
	public class ReservationDto
	{
		public Guid Id { get; set; }
		public Guid ParkingSpotId { get; set; }
		public string EmployeeName { get; set; }
		public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}

