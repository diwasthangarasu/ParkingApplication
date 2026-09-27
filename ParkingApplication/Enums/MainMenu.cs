using System.ComponentModel.DataAnnotations;

namespace ParkingApplication.Enums;

public enum MainMenu
{
    [Display (Name = "Exit the application")]
    Exit,

    [Display (Name = "Check in vehicle")]
    CheckInVehicle,

    [Display (Name = "Check out vehicle")]
    CheckOutVehicle,
}
