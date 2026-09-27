using ParkingApplication.Model;
using ParkingApplication.Service;
using ParkingApplication.View;

namespace ParkingApplication.Controller;

public class DashboardController
{
    private readonly TicketAllocation _ticketAllocation;
    private readonly CheckoutService _checkoutService;
    private readonly LiveSnapShot _liveSnapShot;
    private List<string>? _list;
    private readonly Notifier _notifier;

    public DashboardController(TicketAllocation ticketAllocation, CheckoutService checkoutService, Notifier notifier, LiveSnapShot liveSnapShot)
    {
        _ticketAllocation = ticketAllocation;
        _checkoutService = checkoutService;
        _notifier = notifier;
        this._liveSnapShot = liveSnapShot;
        _notifier._notify += Lists;
        _notifier._notify += Notification.DisplayNotification;
        Lists(null, new(null, null, false));

        Task t = RunAsync();
    }

    public async void Lists(object? sender, Notifier.NotificationArgs args)
    {
        this._list = await _liveSnapShot.GetSnapshot();
    }

    public void Render()
    {
        if (this._list != null)
        {
            Dashboard.DisplayDashboard(this._list);
        }
    }

    public async Task RunAsync()
    {
        while (true)
        {
            this.Render();

            await Task.Delay(1000);
        }
    }
}
