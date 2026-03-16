
namespace housingCooperative.Infrastructures.HostBuilder
{

    public interface IStartup 
    {
        WebApplicationBuilder ConfigureServices();
        void MigrateDBContext(WebApplication app);
        WebApplication Configure(WebApplication app);
        
    } 
}