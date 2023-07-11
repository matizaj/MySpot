using System;
namespace MySpot.Api.Exceptions
{
    public sealed class InvalidLicensePlateException : CustomException
    {
        public InvalidLicensePlateException(string licensePlate) : base($"Invalid license plate: {licensePlate}")
        {
        }
    }
}

