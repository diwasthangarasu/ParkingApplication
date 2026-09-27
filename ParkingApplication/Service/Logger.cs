using ParkingApplication.Repository;

namespace ParkingApplication.Service;

public class Logger
{
    private readonly LoggerRepository _loggerRepository;
    private readonly Notifier _notifier;

    public Logger(LoggerRepository loggerRepository, Notifier notifier)
    {
        this._loggerRepository = loggerRepository;
        this._notifier = notifier;
        this._notifier._notify += Log;
    }

    public async void Log(object? sender, Notifier.NotificationArgs args)
    {
        string message;

        if (args.IsCheckIn)
        {
            message =
                $"CHECK-IN | " +
                $"License Plate: {args.Ticket?.LicensePlate} | " +
                $"Vehicle: {args.Ticket?.VehicleType} | " +
                $"Level: {args.Ticket?.Level} | " +
                $"Slot: {args.Ticket?.SlotNumber}";
        }
        else
        {
            message =
                $"CHECK-OUT | " +
                $"License Plate: {args.Ticket?.LicensePlate} | " +
                $"Vehicle: {args.Ticket?.VehicleType} | " +
                $"Level: {args.Ticket?.Level} | " +
                $"Slot: {args.Ticket?.SlotNumber}";
        }

        await _loggerRepository.Log(message);
    }
    
}
