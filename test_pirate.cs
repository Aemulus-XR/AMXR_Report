using System;
using System.Globalization;
using AemulusConnect.Helpers;
using AemulusConnect.Properties;

class Program
{
    static void Main()
    {
        Console.WriteLine($"Before SetCulture: {CultureInfo.CurrentUICulture.Name}");
        Console.WriteLine($"Before Resources: {Resources.Settings_WindowTitle}");
        
        LocalizationHelper.SetCulture("en-PIRATE");
        
        Console.WriteLine($"After SetCulture: {CultureInfo.CurrentUICulture.Name}");
        Console.WriteLine($"After Resources.Culture: {Resources.Culture?.Name ?? "null"}");
        Console.WriteLine($"After Resources: {Resources.Settings_WindowTitle}");
    }
}
