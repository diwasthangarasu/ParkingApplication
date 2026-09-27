using ParkingApplication.Model;
using ParkingApplication.Repository;

namespace ParkingApplication.Service;

public class CheckoutService
{
    private readonly Parking _parking;
    private readonly TicketRepository _ticketRepository;
    private readonly Notifier _notifier;

    public CheckoutService(Parking parking, TicketRepository ticketRepository, Notifier notifier)
    {
        this._parking = parking;
        this._ticketRepository = ticketRepository;
        this._notifier = notifier;
    }

    public async Task<bool> CheckOutById(Guid ticketId)
    {
        Ticket? ticket = await this._ticketRepository.GetTicketById(ticketId);

        if (ticket is null)
        {
            Console.WriteLine("Failed to close.");
            return false;
        }

        bool result = await this.CloseTicket(ticket);
        if (!result)
        {
            Console.WriteLine("Failed to close.");
        }

        return true;
    }

    public async Task<bool> CheckOutByVehicleNumber(string number)
    {
        Ticket? ticket = await this._ticketRepository.GetTicketByVehicleNumber(number);

        if (ticket is null)
        {
            Console.WriteLine("Failed to close.");
            return false;
        }

        bool result = await this.CloseTicket(ticket);
        if (!result)
        {
            Console.WriteLine("Failed to close.");
        }

        return true;
    }

    private async Task<bool> CloseTicket(Ticket ticket)
    {
        ParkingSlot? slot = await this._parking.GetSlot(ticket.TicketId);
        if (slot is null)
        {
            return false;
        }
        slot.VacateSlot();
        ticket.ExitTime = DateTime.UtcNow;

        this._notifier.OnNotify(slot, ticket, false);

        return true;
    }
}
