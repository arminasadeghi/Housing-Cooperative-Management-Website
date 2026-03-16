

namespace housingCooperative.Infrastructures.HostBuilder
{

    
    public sealed class BooxellWebAppBuilder 
    {
        
        public static IBooxellHostBuilder Create(string[] args)
        {
            return new InnerImpBooxellHostBuilder(args); 
        }

        private sealed class InnerImpBooxellHostBuilder : IBooxellHostBuilder , IBooxellAppBuilder
        {
               private WebApplicationBuilder builder ;
               private WebApplication app ; 
               private IStartup startup ;
               private IStartup startupTest ;
               private string [] args ;

 
               public InnerImpBooxellHostBuilder(string[] args)
               {
                  this.args = args ; 
                  builder = WebApplication.CreateBuilder(args);
               }
       
               public IBooxellHostBuilder ConfigureLogger()
               {
                   
       
                   // Config Logger
                   var configuration = BooxellConfigurationBuilder.GetConfiguration(args);
                   Log.Logger = Program.CreateSerilogLogger(configuration);
       
       
                   
                   // Config Host
                   builder.Host
                       .ConfigureAppConfiguration(x => x.AddConfiguration(configuration));
       
       
                   return this;
               }
       
               public IBooxellHostBuilder UseSerilog()
               {
                   builder.Host.UseSerilog();
                   return this ; 
               }
       
               public IBooxellHostBuilder UseStartUp<T>() where T : IStartup
               {
                   
                   startup = (IStartup)Activator.CreateInstance(typeof(T) , builder);
       

                   // Config WebHostApplication
                   builder = startup.ConfigureServices();
                   Log.Information("app is configuring ({AppName})", Program.Name);
       
       
                   return this ;
               }

       
               public IBooxellAppBuilder BuildWebHost()
               {
                   app = builder.Build();
                   startup.Configure(app);
                   return this ;
               }
       
               public void RunApplication()
               {
                   Log.Information("Starting web app ({AppName})...", Program.Name);
                   app.Run();
               }

               public IBooxellAppBuilder MigrateDbContext()
               {

                   startup.MigrateDBContext(app);
                   return this ; 
               }

        }
        

    }
   
}