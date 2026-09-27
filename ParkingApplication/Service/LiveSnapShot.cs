using ParkingApplication.Enums;
using ParkingApplication.Model;
using ParkingApplication.Repository;

namespace ParkingApplication.Service;

public class LiveSnapShot
{
    private readonly Parking _parking;

    public LiveSnapShot(Parking parking)
    {
        _parking = parking;
    }

    public async Task<List<string>> GetSnapshot()
    {
        List<string> snapshot = new();

        int totalOccupied = 0;
        int totalSlots = 0;

        for (int levelNumber = 1; levelNumber <= 3; levelNumber++)
        {
            IReadOnlyList<ParkingSlot> slots = await _parking.GetLevel(levelNumber);

            int occupied = CalculateOccupiedCount(slots);

            totalOccupied += occupied;
            totalSlots += slots.Count;

            string slotStatus = BuildSlotStatus(slots);

            snapshot.Add($"Level {levelNumber} " + $"({occupied}/{slots.Count}) " + $"Entry: Open " + $"Exit: Idle " + $"{slotStatus}");
        }

        snapshot.Add(string.Empty);

        snapshot.Add($"Totals: {totalOccupied}/{totalSlots} " + $"Open gate cycles: 3 " + $"Pending CSV rows: 4");

        snapshot.Add(string.Empty);

        snapshot.Add("C = Car, M = Motorcycle, V = Van, [ ] = empty.");

        return snapshot;
    }

    private int CalculateOccupiedCount(IReadOnlyList<ParkingSlot> slots)
    {
        int count = 0;

        foreach (ParkingSlot slot in slots)
        {
            if (slot.IsOccupied)
            {
                count++;
            }
        }

        return count;
    }

    private string BuildSlotStatus(IReadOnlyList<ParkingSlot> slots)
    {
        List<string> result = new();

        foreach (ParkingSlot slot in slots)
        {
            string value;

            if (!slot.IsOccupied)
            {
                value = "[ ]";
            }
            else
            {
                value = slot.VehicleType switch
                {
                    VehicleTypes.Car => "[C]",
                    VehicleTypes.Motorcycle => "[M]",
                    VehicleTypes.Van => "[V]",
                    _ => "[?]"
                };
            }

            result.Add(value);
        }

        return string.Join("", result);
    }
}