using ParkingApplication.Enums;
using ParkingApplication.Model;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ParkingApplication.Repository;

public class TicketRepository
{
    private readonly CSVWriter _csvWriter;
    private List<Ticket> _tickets = new ();

    public TicketRepository(CSVWriter writer)
    {
        this._csvWriter = writer;
    }

    public async Task AddTicket(Ticket ticket)
    {
        this._tickets.Add(ticket);
        await _csvWriter.QueueTicket(ticket);
    }

    public async Task LoadTickets()
    {
        List<Ticket> tickets = await _csvWriter.LoadTickets();

        _tickets.Clear();
        _tickets.AddRange(tickets);
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