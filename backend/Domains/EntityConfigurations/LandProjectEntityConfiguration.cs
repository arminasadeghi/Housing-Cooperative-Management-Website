using housingCooperative.Domains.Entities;

namespace housingCooperative.Domains.EntityConfigurations
{
    public class LandProjectEntityConfiguration : IEntityTypeConfiguration<LandProjectEntity>
    {
        public LandProjectEntityConfiguration()
        {
        }

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LandProjectEntity> builder)
        {


            builder.HasKey(x => x.Id);
            builder.Ignore(x => x._Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsVisibled).HasDefaultValue(true);
            builder.Ignore(x => x.ImageList);
            builder.HasMany(c => c.Phases).WithOne(c => c.LandProject).HasForeignKey(c => c.ProjectId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(c => c.Plots).WithOne(c => c.LandProject).HasForeignKey(c => c.ProjectId).OnDelete(DeleteBehavior.NoAction);
            builder.ToTable($"tbl{nameof(LandProjectEntity)}");


        }
    }
}