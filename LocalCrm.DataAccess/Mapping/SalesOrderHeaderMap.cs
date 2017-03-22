using LocalCrm.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace LocalCrm.DataAccess.Mapping
{
    public class SalesOrderHeaderMap : EntityTypeConfiguration<SalesOrderHeader>
    {
        public SalesOrderHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.SalesOrderId);

            // Properties
            this.Property(t => t.OrderNo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_OrderNo")));

            this.Property(t => t.OrderDate)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_OrderDate")));

            this.Property(t => t.TrackNumber)
                .HasMaxLength(30);


            // Table & Column Mappings
            this.ToTable("SalesOrderHeader");
            this.Property(t => t.SalesOrderId).HasColumnName("SalesOrderId");
            this.Property(t => t.OrderNo).HasColumnName("OrderNo");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.OrderTotal).HasColumnName("OrderTotal");
            this.Property(t => t.SalesPersonId).HasColumnName("SalesPersonId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.OrderPrice).HasColumnName("OrderPrice");
            this.Property(t => t.Prepayment).HasColumnName("Prepayment");
            this.Property(t => t.ShipDate).HasColumnName("ShipDate");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.TransportCompanyId).HasColumnName("TransportCompanyId");
            this.Property(t => t.ShipingCost).HasColumnName("ShipingCost");
            this.Property(t => t.ReceiptDate).HasColumnName("ReceiptDate");
            this.Property(t => t.ReceivedForDelivery).HasColumnName("ReceivedForDelivery");
            this.Property(t => t.ReceivedForOrder).HasColumnName("ReceivedForOrder");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.TrackNumber).HasColumnName("TrackNumber");
            this.Property(t => t.OrderComplitDate).HasColumnName("OrderComplitDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            

            // Relationships
            this.HasOptional(t => t.City)
                .WithMany(t => t.SalesOrderHeaders)
                .HasForeignKey(d => d.CityId);

            this.HasOptional(t => t.Customer)
                .WithMany(t => t.SalesOrderHeaders)
                .HasForeignKey(d => d.CustomerId);

            this.HasOptional(t => t.SalesPerson)
                .WithMany(t => t.SalesOrderHeaders)
                .HasForeignKey(d => d.SalesPersonId);

            this.HasOptional(t => t.TransportCompany)
                .WithMany(t => t.SalesOrderHeaders)
                .HasForeignKey(d => d.TransportCompanyId);
            /**/
        }
    }
}
