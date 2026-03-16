## Example

dbCOntext

    public class housingCooperativeDBContext : Library.DDD.DataAccessLayer.DAL.BaseDbContext, IDb<housingCooperativeDBContext>
    {

        public housingCooperativeDBContext(DbContextOptions<housingCooperativeDBContext> opto) : base(opto) { }


        public housingCooperativeDBContext GetDbContext() => this;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(housingCooperativeDBContext).Assembly);
        }

    }

add below code to startup for configing DBContext and UnitOfWork

            builder.Services.AddDbContext<housingCooperativeDBContext>(opt =>
            {
                var connection = string.Format(Configuration.GetConnectionString("SqlDefault"),
                    Configuration["ApiUrls:Sql-Data"]);
                opt.UseSqlServer(connection);
            });

            builder.Services.AddUnitOfWork<housingCooperativeDBContext>();
