using App.Microservices.Framework.ConfigOptions;

namespace App.Microservices.AuthServer.Configs;

public class AppSettings
{
    public ConnectionStringsOption ConnectionStrings { get; set; }
    public AuthenticationOption Authentication { get; set; }
}



