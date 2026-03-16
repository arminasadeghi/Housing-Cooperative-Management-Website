







try
{
    
    var asm = Assembly.GetEntryAssembly(); if (asm is not null)  Program.Name = asm.GetName()?.Name ?? "";



    BooxellWebAppBuilder
        .Create(args)
        .ConfigureLogger()
        .UseSerilog()
        .UseStartUp<Startup>()
        .BuildWebHost()
        .MigrateDbContext()
        .RunApplication();



}
catch (System.Exception exp)
{

   if (exp.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
   {
      throw;
   }

   Log.Error<string>(exp, "failed at running service ({AppName})", Program.Name);
}
finally
{
    Log.CloseAndFlush();
}





public partial class Program
{
    public static string Name = "housingCooperative" ; 
    public static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {

        var seqServerUrl = configuration["Logging:Serilog:SeqServerUrl"];
        var logstashUrl = configuration["Logging:Serilog:LogstashgUrl"];
        var elasticUrl = configuration.GetValue<string>("Elastic:Uri");
        var elasticUsername = configuration.GetValue<string>("Elastic:Username");
        var elasticPassword = configuration.GetValue<string>("Elastic:Password");
        var logConfig = new LoggerConfiguration();
        var env = configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");
        logConfig = logConfig
            .Enrich.WithProperty("AppName", Program.Name)
            .Enrich.WithProperty("Environment", env)
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMachineName()
            // .Enrich.WithElasticApmCorrelationInfo()
            .Enrich.FromLogContext()
            .WriteTo.Console();
        if (!string.IsNullOrEmpty(elasticUrl))
            logConfig = logConfig.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl))
            {
                CustomFormatter = new Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter(), //new EcsTextFormatter(),
                ModifyConnectionSettings = x => x.BasicAuthentication(elasticUsername, elasticPassword),
                IndexFormat = "booxell-app-logs",
                AutoRegisterTemplate = true,
                // IndexDecider
            });
        if (!string.IsNullOrWhiteSpace(seqServerUrl))
            logConfig = logConfig.WriteTo.Seq(seqServerUrl);
        if (!string.IsNullOrWhiteSpace(logstashUrl))
            logConfig = logConfig.WriteTo.Http(logstashUrl);

        logConfig = logConfig.ReadFrom.Configuration(configuration);
        return logConfig.CreateLogger();
    }


}

