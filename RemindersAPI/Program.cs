using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RemindersAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(configure => configure.ValidateScopes = false)
                .UseStartup<Startup>();
    }
}