namespace housingCooperative.Infrastructures.HostBuilder
{
    public interface IBooxellAppBuilder
    {
        IBooxellAppBuilder MigrateDbContext();
        void RunApplication();
    }
}