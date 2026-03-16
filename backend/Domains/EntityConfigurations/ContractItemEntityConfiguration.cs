using housingCooperative.Domains.Entities;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class ContractItemEntityConfiguration : IEntityTypeConfiguration<ContractItemEntity>
    {
        public ContractItemEntityConfiguration()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ContractItemEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.HasOne(x =>x.Contract).WithMany(x => x.Items).HasForeignKey(x => x.ContractId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.PaidItems).WithOne(x => x.ContractItem).HasForeignKey(x => x.ContractItemId).OnDelete(DeleteBehavior.NoAction);
            builder.ToTable($"tbl{nameof(ContractItemEntity)}");


        }
    }
}