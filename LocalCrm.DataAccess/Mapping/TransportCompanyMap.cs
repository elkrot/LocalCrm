using LocalCrm.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LocalCrm.DataAccess.Mapping
{
    public class TransportCompanyMap : EntityTypeConfiguration<TransportCompany>
    {
        public TransportCompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.TransportCompanyId);

            // Properties
            this.Property(t => t.TransportCompanyName)
                .IsRequired()
                .HasMaxLength(50).HasColumnAnnotation(
        IndexAnnotation.AnnotationName,
        new IndexAnnotation(
            new IndexAttribute("IX_TransportCompanyName") { IsUnique = true }));

            // Table & Column Mappings
            this.ToTable("TransportCompany");
            this.Property(t => t.TransportCompanyId).HasColumnName("TransportCompanyId");
            this.Property(t => t.TransportCompanyName).HasColumnName("TransportCompanyName");
        }
    }
}
