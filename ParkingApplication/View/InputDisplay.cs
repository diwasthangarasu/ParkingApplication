namespace ParkingApplication.View;

public static class InputDisplay
{
    public static string GetStringInput(string prompt)
    {
        string? input;

        while (true)
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                DisplayView.DisplayMessage("Input cant be empty.");
                continue;
            }

            return input;
        }
    }
}
