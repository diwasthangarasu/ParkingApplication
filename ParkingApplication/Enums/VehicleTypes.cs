using System.ComponentModel.DataAnnotations;

namespace ParkingApplication.Enums;

public enum VehicleTypes
{
    [Display (Name = "Car")]
    Car = 1,
    [Display (Name = "Van")]
    Van,
    [Display (Name = "Motorcycle")]
    Motorcycle
}
