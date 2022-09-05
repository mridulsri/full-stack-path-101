using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection;


public static class LogStartupSetup
{
    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {


        return services;
    }
}
