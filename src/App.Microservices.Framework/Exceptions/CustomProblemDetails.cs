using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Microservices.Framework.Exceptions;

public class CustomProblemDetails: ProblemDetails
{
    public string AdditionalInfo { get; set; }
}
