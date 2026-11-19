using System.Globalization;
using System.Reflection;

// Runs the kiosk's Program.cs and returns everything it printed, one entry per line.
// You do not need to change this file.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace LibraryKiosk.Tests;

public static class KioskRunner
{
    public static string[] Run()
    {
        var output = new StringWriter();
        var originalOut = Console.Out;
        var originalCulture = CultureInfo.CurrentCulture;
        Console.SetOut(output);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            Assembly.Load("LibraryKiosk").EntryPoint!.Invoke(null, [Array.Empty<string>()]);
        }
        finally
        {
            Console.SetOut(originalOut);
            CultureInfo.CurrentCulture = originalCulture;
        }
        return output.ToString().Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.TrimEnd('\r')).ToArray();
    }
}
