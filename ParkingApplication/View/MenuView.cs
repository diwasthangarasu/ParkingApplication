using ParkingApplication.Constants;
using ParkingApplication.View;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WashingMachine.View;

public static class MenuView
{
    public static T GetMenuOption<T>()
        where T : struct, Enum
    {
        RenderConsole.RenderApplication();
        while (true)
        {

            DisplayMenu<T>();
            DisplayView.DisplayMessage(PromptMessages.SelectOption);

            string? input = Console.ReadLine();
            if (int.TryParse(input, out int option) && Enum.IsDefined(typeof(T), option))
            {
                return (T)Enum.ToObject(typeof(T), option);
            }
            else
            {
                DisplayView.DisplayMessage("Invalid option. Please try again.");
            }
        }
    }

    private static void DisplayMenu<T>()
        where T : struct, Enum
    {
        foreach (T option in Enum.GetValues<T>())
        {
            string displayName = GetDisplayName(option);
            DisplayView.DisplayMessage($"{Convert.ToInt32(option)} - {displayName}");
        }
    }

    private static string GetDisplayName<T>(T option) where T : Enum
    {
        MemberInfo memberInfo = typeof(T).GetMember(option.ToString())[0];
        if (memberInfo is not null)
        {
            DisplayAttribute? displayNameAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
            if (displayNameAttribute is not null)
            {
                return displayNameAttribute.Name ?? option.ToString();
            }
        }

        return option.ToString();
    }
}