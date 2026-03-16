namespace housingCooperative.Infrastructures.DbContexts
{
    public class housingCooperativeDBContext : BaseDbContext, IDb<housingCooperativeDBContext>
    {

        public housingCooperativeDBContext(DbContextOptions<housingCooperativeDBContext> opto) : base(opto) { }


        public housingCooperativeDBContext GetDbContext() => this;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(housingCooperativeDBContext).Assembly);
        }

    }
}