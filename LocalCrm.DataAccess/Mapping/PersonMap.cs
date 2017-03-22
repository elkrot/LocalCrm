using LocalCrm.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LocalCrm.DataAccess.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.PersonId);

            // Properties


            this.Property(t => t.FirstName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50).HasColumnAnnotation(
        IndexAnnotation.AnnotationName,
        new IndexAnnotation(
            new IndexAttribute("IX_FIO",1) { IsUnique = true }));

            this.Property(t => t.MiddleName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50).HasColumnAnnotation(
        IndexAnnotation.AnnotationName,
        new IndexAnnotation(
            new IndexAttribute("IX_FIO", 2) { IsUnique = true }));

            this.Property(t => t.LastName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_FIO", 3) { IsUnique = true }));


            this.Property(t => t.Phone)
            .IsFixedLength()
            .HasMaxLength(150);

            this.Property(t => t.AdditionalContactInfo)
            .IsFixedLength()
            .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Person");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.AdditionalContactInfo).HasColumnName("AdditionalContactInfo");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
