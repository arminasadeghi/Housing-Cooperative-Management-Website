using housingCooperative.Domains.Entities;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class PhaseEntityConfiguration : IEntityTypeConfiguration<PhaseEntity>
    {
        public PhaseEntityConfiguration()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PhaseEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.Ignore(x => x.ImageList);
            builder.Property(x => x.ProjectId).IsRequired();
            builder.ToTable($"tbl{nameof(PhaseEntity)}");


        }
    }
}