using BNA.EF1.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BNA.EF1.Infrastructure.Database.Configurations
{
    public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Apellido)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Cuil)
                .IsRequired();

            builder.HasMany(c => c.Cuentas)
                .WithOne()
                .HasForeignKey(cu => cu.ClienteId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
