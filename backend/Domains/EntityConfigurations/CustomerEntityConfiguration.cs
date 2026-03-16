using housingCooperative.Domains.Entities;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public CustomerEntityConfiguration()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CustomerEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.HasMany(c => c.Contracts).WithOne(c => c.Customer).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.ToTable($"tbl{nameof(CustomerEntity)}");


        }
    }
}