namespace ParkingApplication.View;

public static class Dashboard
{
    public static void DisplayDashboard(List<string> data)
    {
        RenderConsole.RenderDashboard();

        foreach (string d in data)
        {
            Console.WriteLine(d);
        }

        RenderConsole.SetCursorBack();
    }
}
