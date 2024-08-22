using BNA.EF1.Domain.Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BNA.EF1.Infrastructure.Database.Configurations
{
    internal class ExampleClassConfiguration : IEntityTypeConfiguration<ExampleClass>
    {
        public void Configure(EntityTypeBuilder<ExampleClass> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.ExampleField)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.InternalInfo)
                .HasMaxLength(8)
                .IsFixedLength()
                .IsRequired();
        }
    }
}
