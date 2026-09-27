namespace ParkingApplication.View;

public static class RenderConsole
{
    private static readonly int _dashboardStartRow = 0;

    private static int _dashboardHeight => Console.WindowHeight / 3;

    private static int _applicationStartRow => _dashboardHeight;

    private static int _applicationStartColumn => 0;

    private static int _notificationStartRow => _dashboardHeight;

    private static int _notificationStartColumn => Console.WindowWidth * 7 / 10;

    private static  (int, int) _recentCursorPosition;

    public static void RenderDashboard()
    {
        (_recentCursorPosition.Item1, _recentCursorPosition.Item2) = Console.GetCursorPosition();

        ClearDashboard();

        Console.SetCursorPosition(0, 0);
    }

    public static void RenderApplication()
    {
        (_recentCursorPosition.Item1, _recentCursorPosition.Item2) = Console.GetCursorPosition();

        ClearApplication();

        Console.SetCursorPosition(_applicationStartColumn, _applicationStartRow);
    }

    public static void RenderNotification()
    {
        (_recentCursorPosition.Item1, _recentCursorPosition.Item2) = Console.GetCursorPosition();

        ClearNotification();

        Console.SetCursorPosition(_notificationStartColumn, _notificationStartRow);
    }

    public static void SetCursorBack()
    {
        Console.SetCursorPosition(_recentCursorPosition.Item1, _recentCursorPosition.Item2);
    }

    private static void ClearDashboard()
    {
        for (int row = 0; row < _dashboardHeight; row++)
        {
            Console.SetCursorPosition(0, row);

            Console.Write(new string(' ', Console.WindowWidth));
        }
    }

    private static void ClearApplication()
    {
        int applicationWidth = _notificationStartColumn - _applicationStartColumn;

        for (int row = _applicationStartRow; row < Console.WindowHeight; row++)
        {
            Console.SetCursorPosition(_applicationStartColumn, row);

            Console.Write(new string(' ', applicationWidth));
        }
    }

    private static void ClearNotification()
    {
        for (int row = _notificationStartColumn; row < Console.WindowHeight; row++)
        {
            Console.SetCursorPosition(_notificationStartColumn, row);

            Console.Write(new string(' ', Console.WindowWidth - _notificationStartColumn));
        }
    }
}