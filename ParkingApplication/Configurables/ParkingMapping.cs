namespace ParkingApplication.Configurables;

public static class ParkingMapping
{
    public static readonly Dictionary<int, int> parkingMapping = new()
    {
        { 1, 20 },
        { 2, 15 },
        { 3, 10 }
    };
}
