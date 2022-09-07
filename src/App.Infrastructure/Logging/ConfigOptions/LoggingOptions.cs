using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Logging.ConfigOptions
{
    public class LoggingOptions
    {
        public const string Logging = "Logging";
        public Dictionary<string, string> LogLevel { get; set; }

        public FileOptions File { get; set; }
    }
}
