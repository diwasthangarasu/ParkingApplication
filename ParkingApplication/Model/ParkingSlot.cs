using ParkingApplication.Enums;

namespace ParkingApplication.Model;

public class ParkingSlot
{
    public int SlotNumber { get; init; }

    public bool IsOccupied { get; private set; }

    public Guid? TicketId { get; private set; }

    public VehicleTypes? VehicleType { get; private set; }

    public ParkingSlot(int slotNumber)
    {
        this.SlotNumber = slotNumber;
        this.IsOccupied = false;
    }

    public void AssignTicket(Guid ticketId, VehicleTypes vehicleType)
    {
        this.TicketId = ticketId;
        this.VehicleType = vehicleType;
        this.IsOccupied = true;
    }

    public void VacateSlot()
    {
        this.TicketId = null;
        this.VehicleType = null;
        this.IsOccupied = false;
    }
}
