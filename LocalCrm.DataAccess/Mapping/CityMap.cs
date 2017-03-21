using LocalCrm.Model;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalCrm.DataAccess.Mapping
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            // Primary Key
            this.HasKey(t => t.CityId);

            // Properties
            this.Property(t => t.CityName)
                .HasMaxLength(50).HasColumnAnnotation(
        IndexAnnotation.AnnotationName,
        new IndexAnnotation(
            new IndexAttribute("IX_CityName") { IsUnique = true }));

            // Table & Column Mappings
            this.ToTable("City");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.CityName).HasColumnName("CityName");
        }
    }
}
