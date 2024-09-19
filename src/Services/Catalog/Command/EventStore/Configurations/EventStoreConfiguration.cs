
using System.Text.Json;
using cShop.Contracts.Abstractions;
using cShop.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EventStore.Configurations;

public class EventStoreConfiguration : IEntityTypeConfiguration<StoreEvent>
{
    public void Configure(EntityTypeBuilder<StoreEvent> builder)
    {
        builder.HasKey(e => new { e.AggregateId, e.Version });

        builder.Property(e => e.AggregateId).IsRequired();

        builder.Property(e => e.AggregateType).IsRequired();

        builder.Property(e => e.EventType).IsRequired();

        builder.Property(e => e.Version).IsRequired();

        builder.Property(e => e.CreatedAt).IsRequired();
        
        builder.Property(e => e.Event).HasConversion<StoreEventConverter>(
        ).IsRequired();

    }
}