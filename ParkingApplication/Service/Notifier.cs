using ParkingApplication.Model;

namespace ParkingApplication.Service;

public class Notifier
{
    public event EventHandler<NotificationArgs>? _notify;

    public class NotificationArgs : EventArgs
    {
        public NotificationArgs(ParkingSlot? slot, Ticket? ticket, bool isCheckIn)
        {
            this.Slot = slot;
            this.Ticket = ticket;
            this.IsCheckIn = isCheckIn;
        }

        public ParkingSlot? Slot { get; set; }
        public Ticket? Ticket { get; set; }
        public bool IsCheckIn { get; set; }
    }

    public void OnNotify(ParkingSlot? slot, Ticket? ticket, bool isCheckIn)
    {
        this._notify?.Invoke(this, new NotificationArgs(slot, ticket, isCheckIn));
    }
}
