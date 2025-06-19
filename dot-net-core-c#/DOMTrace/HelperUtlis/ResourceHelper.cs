using System.IO;
using System.Reflection;

namespace DOMTrace.HelperUtlis;

public class ResourceHelper
{
    public static string ReadEmbeddedScript(string fullResourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(fullResourceName);
        if (stream == null)
            throw new FileNotFoundException($"Embedded resource '{fullResourceName}' not found.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}