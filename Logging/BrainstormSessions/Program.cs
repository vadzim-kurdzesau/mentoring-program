using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.EmailPickup;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                    loggerConfiguration.WriteTo.EmailPickup(
                        fromEmail: "testEmail@test.com",
                        toEmail: "Vadzim_Kurdzesau@epam.com",
                        pickupDirectory: @"C:\Users\Vadzim_Kurdzesau\source\repos\Learning\MentoringProgram\Logging\BrainstormSessions\Logs\Emailpickup\",
                        subject: "Log",
                        fileExtension: ".email",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug
                    );
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
