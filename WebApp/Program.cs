using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxRequestLineSize = 8192 * 16;
                    options.AllowSynchronousIO = true;
                });

    }
}
