using housingCooperative.Domains.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class ContractEntityConfiguration : IEntityTypeConfiguration<ContractEntity>
    {
        public ContractEntityConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ContractEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.HasOne(c => c.Customer).WithMany(c => c.Contracts).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.Plot).WithOne(c => c.Contract).HasForeignKey<ContractEntity>(c => c.PlotId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(c => c.LandProject).WithMany().HasForeignKey(c => c.ProjectId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(c => c.Items).WithOne(c => c.Contract).HasForeignKey(c => c.ContractId).OnDelete(DeleteBehavior.NoAction);
            builder.ToTable($"tbl{nameof(ContractEntity)}");
            


        }
    }
}