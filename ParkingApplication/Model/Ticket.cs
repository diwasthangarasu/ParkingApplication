using ParkingApplication.Enums;

namespace ParkingApplication.Model;

public class Ticket
{
    public Ticket(Guid ticketID, string licensePlate, VehicleTypes vehicleType, DateTime entryTime, int level, int slotNumber)
    {
        this.TicketId = ticketID;
        this.LicensePlate = licensePlate;
        this.VehicleType = vehicleType;
        this.EntryTime = entryTime;
        this.Level = level;
        this.SlotNumber = slotNumber;
    }

    public Guid TicketId { get; init; }

    public string LicensePlate { get; init; }

    public VehicleTypes VehicleType { get; init; }

    public DateTime EntryTime { get; init; }

    public DateTime? ExitTime { get; set; }

    public int Level { get; init; }

    public int SlotNumber { get; init; }
}
