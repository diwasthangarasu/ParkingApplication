using ParkingApplication.Configurables;
using ParkingApplication.Enums;
using ParkingApplication.Model;

namespace ParkingApplication.Repository;

public class Parking
{
    public List<ParkingLevel> _levels { get; private set; }

    public Dictionary<int, int> parkingMapping = ParkingMapping.parkingMapping;

    public Parking()
    {
        this._levels = new();
        foreach (KeyValuePair<int, int> kvp in parkingMapping)
        {
            this._levels.Add(new ParkingLevel(kvp.Key, kvp.Value));
        }
    }

    public async Task<IReadOnlyList<ParkingSlot>> GetLevel(int level)
    {
        Console.WriteLine(this._levels.Count);
        return this._levels[level - 1].Slots;
    }

    public async Task<ParkingSlot?> GetSlot(Guid ticketId)
    {
        foreach (ParkingLevel parkingLevel in _levels)
        {
            return parkingLevel.Slots.FirstOrDefault(slot => slot.TicketId == ticketId);
        }

        return null;
    }

    public async Task AssignTicketToSlot(Ticket ticket)
    {
        ParkingSlot slot = this._levels[ticket.Level - 1].Slots[ticket.SlotNumber - 1];
        slot.AssignTicket(ticket.TicketId, ticket.VehicleType);
        
    }
}
