using ParkingApplication.Service;

namespace ParkingApplication.View;

public static class Notification
{
    public static void DisplayNotification(object? sender, Notifier.NotificationArgs args)
    {
        RenderConsole.RenderNotification();

        if (args.IsCheckIn)
        {
            Console.WriteLine($"{args.Ticket.LicensePlate} check in successful");
        }
        Console.WriteLine($"{args.Ticket.LicensePlate} check out successful");

        RenderConsole.SetCursorBack();
    }
}
