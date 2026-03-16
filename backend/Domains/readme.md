# Domains

all the entites of the app go here

# Implementation

Each entity should be extended from **BaseEntity** , **ICreatedDateTime** , **IModifiedDateTime**

all properties of entity should be private set.

Each entity should be convert by Mapster to diffrent DTOs.

For Each Entity we create EntityConfiguration and put it configuration of SQL SERVER migration config

### **notice** : We can not change properties directly

## NamingPattern

    housingCooperativeEntity.cs

## Example

### Entity

    public class TestEntity : BaseEntity<string>, ICreatedDateTime, IModifiedDateTime
    {



        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt {get; private set; }
        public DateTime? ModifiedAt {get; private set; }



        public TestEntity(string name, string phone, DateTime createdAt, DateTime? modifiedAt)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Phone = phone;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }




    }

### EntityConfiguration

    public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public TestEntityConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {

            //builder.Ignore(x => x._Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getDate()");
            builder.ToTable($"tbl{nameof(TestEntity)}");


        }
    }

### DBContext Migrate && Seed Example

        // Get UnitofWork and check if unitofwork is not null then inject it into StartUp

        using (var serviceScope = app.Services.CreateScope())
        {

           var unitOfWork = serviceScope.ServiceProvider.GetService<IUnitOfWork<housingCooperativeDBContext>>();

           if(!sqlServerUseInMemoryDB && !unitOfWork.DbContext.Database.IsInMemory())
           {

                   unitOfWork.DbContext.Database.Migrate();
                   // Seed the database.

           }


        }
