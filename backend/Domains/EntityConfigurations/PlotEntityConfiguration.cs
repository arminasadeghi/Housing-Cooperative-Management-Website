using housingCooperative.Domains.Entities;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class PlotEntityConfiguration : IEntityTypeConfiguration<PlotEntity>
    {
        public PlotEntityConfiguration()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PlotEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Ignore(x => x.InstalmentCount);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.Property(x => x.ProjectId).IsRequired();
            builder.ToTable($"tbl{nameof(PlotEntity)}");


        }
    }
}