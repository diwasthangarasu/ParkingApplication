using ParkingApplication.Controller;
using ParkingApplication.Repository;
using ParkingApplication.Service;
using ParkingApplication.View;

namespace ParkingApplication;

public class Program
{
    public static void Main()
    {
        Parking parking = new Parking();
        CSVWriter writer = new CSVWriter("Ticket.csv");
        TicketRepository ticketRepository = new TicketRepository(writer);
        _ = ticketRepository.LoadTickets();
        Notifier notifier = new ();
        TicketAllocation ticketAllocation = new TicketAllocation(parking, ticketRepository, notifier);
        CheckoutService checkoutService = new CheckoutService(parking, ticketRepository, notifier);
        ParkingController parkingController = new ParkingController(ticketAllocation, checkoutService);
        LiveSnapShot liveSnapShot = new LiveSnapShot(parking);
        DashboardController dashboardController = new DashboardController(ticketAllocation, checkoutService, notifier, liveSnapShot);
        LoggerRepository loggerRepository = new("log.txt");
        Logger logger = new Logger(loggerRepository, notifier);
        parkingController.Run();
        Console.ReadKey();
    }
}
