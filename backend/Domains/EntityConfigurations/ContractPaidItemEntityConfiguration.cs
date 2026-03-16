using housingCooperative.Domains.Entities;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class ContractPaidItemEntityConfiguration : IEntityTypeConfiguration<ContractPaidItemEntity>
    {
        public ContractPaidItemEntityConfiguration()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ContractPaidItemEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.HasOne(x =>x.ContractItem).WithMany(x => x.PaidItems).HasForeignKey(x => x.ContractItemId).OnDelete(DeleteBehavior.NoAction);
            builder.ToTable($"tbl{nameof(ContractPaidItemEntity)}");


        }
    }
}