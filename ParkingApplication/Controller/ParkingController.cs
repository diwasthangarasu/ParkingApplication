using ParkingApplication.Enums;
using ParkingApplication.Service;
using ParkingApplication.View;
using WashingMachine.View;

namespace ParkingApplication.Controller;

public class ParkingController
{
    private readonly TicketAllocation _ticketAllocation;
    private readonly CheckoutService _checkOutService;
    public ParkingController(TicketAllocation ticketAllocation, CheckoutService checkoutService)
    {
        this._ticketAllocation = ticketAllocation;
        this._checkOutService = checkoutService;
    }

    public void Run()
    {
        while (true)
        {
            MainMenu mainMenuOption = MenuView.GetMenuOption<MainMenu>();

            switch (mainMenuOption)
            {
                case MainMenu.Exit:
                    return;
                case MainMenu.CheckInVehicle:
                    Task checkIn = this.CheckInVehicle();
                    break;
                case MainMenu.CheckOutVehicle:
                    Task CheckOut = this.CheckOutVehicle();
                    break;
                default:
                    DisplayView.DisplayMessage("Invaild option");
                    break;
            }
        }
    }

    private async Task CheckInVehicle()
    {
        VehicleTypes vehicleType = MenuView.GetMenuOption<VehicleTypes>();
        string licensePlate = InputDisplay.GetStringInput("Enter vehicle number");

        bool result = await this._ticketAllocation.GenerateTicket(licensePlate, vehicleType);
        if (!result)
        {
            DisplayView.DisplayMessage("No available slot");
        }
    }

    private async Task CheckOutVehicle()
    {
        CheckOutBy type = MenuView.GetMenuOption<CheckOutBy>();
        bool result = false;
        switch (type)
        {
            case CheckOutBy.CheckOutByVechileNumber:
                string vehicleNumber = InputDisplay.GetStringInput("Enter vehicle number");
                result = await this._checkOutService.CheckOutByVehicleNumber(vehicleNumber);
                break;
            case CheckOutBy.CheckOutById:
                Console.WriteLine("Use check out by number");
                break;
            default:
                Console.WriteLine("Try again");
                break;
        }

        if (result)
        {
            DisplayView.DisplayMessage("Slot removed successfully");
        }
    }
}
