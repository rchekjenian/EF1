using BNA.EF1.Domain.Cuentas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BNA.EF1.Infrastructure.Database.Configurations
{
    public sealed class CuentaConfiguration: IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x =>x.Saldo)
                .IsRequired();


            builder.Property(x => x.NumeroCuenta)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);
            builder.Property(x => x.ClienteId)
                .IsRequired();
               
        }
    }
}
