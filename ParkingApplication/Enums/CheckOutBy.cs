using System.ComponentModel.DataAnnotations;

namespace ParkingApplication.Enums;

public enum CheckOutBy
{
    [Display (Name = "Check out by ticket ID")]
    CheckOutById = 1,

    [Display (Name = "Check out by vehicle number")]
    CheckOutByVechileNumber,
}
