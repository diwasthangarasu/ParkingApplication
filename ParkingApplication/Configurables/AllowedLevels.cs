using ParkingApplication.Enums;

namespace ParkingApplication.Configurables;

public static class AllowedLevels
{
    public static readonly Dictionary<VehicleTypes, List<int>> allowedLevels = new()
    {
        { VehicleTypes.Car, new List<int> { 1, 2 } },
        { VehicleTypes.Van, new List<int> { 1 } },
        { VehicleTypes.Motorcycle, new List<int> { 1, 2, 3 } }
    };
}
