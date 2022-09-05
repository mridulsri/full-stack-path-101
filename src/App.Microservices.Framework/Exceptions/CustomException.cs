using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Microservices.Framework.Exceptions;

public class CustomException: Exception
{
    public string AdditionalInfo { get; set; }
    public string Type { get; set; }
    public string Detail { get; set; }
    public string Title { get; set; }
    public string Instance { get; set; }
    public CustomException(string instance)
    {
        Type = "custom-exception";
        Detail = "There was an unexpected error.";
        Title = "Custom Exception";
        AdditionalInfo = "Maybe you can try again in a bit?";
        Instance = instance;
    }
}
