using ParkingApplication.Model;
using System.Security.Cryptography.X509Certificates;

namespace ParkingApplication.Repository;

public class TicketRepository
{
    private List<Ticket> _tickets = new ();

    public async Task AddTicket(Ticket ticket)
    {
        this._tickets.Add(ticket);
    }

    public async Task<Ticket?> GetTicketById(Guid ticketId)
    {
        return _tickets.FirstOrDefault(ticket => ticket.TicketId == ticketId);
    }

    public async Task<Ticket?> GetTicketByVehicleNumber(string number)
    {
        return _tickets.FirstOrDefault(ticket => ticket.LicensePlate.Equals(number, StringComparison.OrdinalIgnoreCase));
    }
}
