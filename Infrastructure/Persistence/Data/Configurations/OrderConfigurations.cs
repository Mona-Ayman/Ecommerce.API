
using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, shipping => shipping.WithOwner());

            builder.HasMany(order => order.OrderItems)
                .WithOne();

            builder.Property(order => order.PaymentStatus).
                HasConversion(
                s => s.ToString(),                            // convert from enum into string to store the enum as a string in db
                s => Enum.Parse<OrderPaymentStatus>(s));        // convert from string into enum to retreive from db

            builder.HasOne(order => order.DeliveryMethods)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);                              //because if the DeliveryMethod was deleted => set its orders into null 
          
            builder.Property(order => order.Subtotal)
                .HasColumnType("decimal(18,3)");
        }
    }
}
