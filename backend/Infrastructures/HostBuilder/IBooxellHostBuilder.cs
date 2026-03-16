namespace housingCooperative.Infrastructures.HostBuilder
{
    public interface IBooxellHostBuilder
    {
        
        IBooxellHostBuilder ConfigureLogger();
        IBooxellHostBuilder UseSerilog();
        IBooxellHostBuilder UseStartUp<T>() where T : IStartup;
        IBooxellAppBuilder BuildWebHost();

    }
}