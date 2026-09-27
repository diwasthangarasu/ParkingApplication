using ParkingApplication.Configurables;
using ParkingApplication.Enums;
using ParkingApplication.Model;
using ParkingApplication.Repository;

namespace ParkingApplication.Service;

public class TicketAllocation
{
    private readonly Parking _parking;
    private readonly Notifier _notifier;

    private readonly TicketRepository _ticketRepository;
    public TicketAllocation(Parking parking, TicketRepository ticketRepository, Notifier notifier)
    {
        this._parking = parking;
        this._ticketRepository = ticketRepository;
        this._notifier = notifier;
    }

    public async Task<bool> GenerateTicket(string licensePlate, VehicleTypes vehicleType)
    {
        (int level, int slotNumber) = await GetAvailableSlot(vehicleType);

        if (level == -1 && slotNumber == -1)
        {
            Console.WriteLine("No available slots for the given vehicle type.");
            return false;
        }

        Ticket ticket = new Ticket(Guid.NewGuid(), licensePlate, vehicleType, DateTime.UtcNow, level, slotNumber);

        await Task.WhenAll(this._ticketRepository.AddTicket(ticket), this._parking.AssignTicketToSlot(ticket));
        this._notifier.OnNotify(null, ticket, true);
        return true;
    }

    private async Task<(int, int)> GetAvailableSlot(VehicleTypes vehicleType)
    {
        IReadOnlyList<int> allowedLevels = AllowedLevels.allowedLevels[vehicleType];

        foreach (int level in allowedLevels)
        {
            IReadOnlyList<ParkingSlot> slots = await _parking.GetLevel(level);
            int slotNumber = await this.GetAvailableSlot(slots);
            if (slotNumber != -1)
            {
                return (level, slotNumber);
            }
        }

        return (-1, -1);
    }

    private async Task<int> GetAvailableSlot(IReadOnlyList<ParkingSlot> slots)
    {
        foreach (ParkingSlot slot in slots)
        {
            if (!slot.IsOccupied)
            {
                return slot.SlotNumber;
            }
        }

        return -1;
    }
}
