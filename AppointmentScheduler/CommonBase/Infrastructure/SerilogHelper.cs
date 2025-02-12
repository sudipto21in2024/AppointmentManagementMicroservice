using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Infrastructure
{
    public static class SerilogHelper
    {
        public static void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
