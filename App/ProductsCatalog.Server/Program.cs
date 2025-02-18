namespace ProductsCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, logging) =>
            {
                var env = hostingContext.HostingEnvironment;
                var config = hostingContext.Configuration;
            })
            .ConfigureServices((hostContext, services) =>{})
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

    }
}
