using BNA.EF1.Application.Common.Specifications.Base;
using BNA.EF1.Domain.Cuentas;
using System.Security.Cryptography.X509Certificates;

namespace BNA.EF1.Application.Cuentas.Specifications
{
    public sealed class CuentaByIdSpecification : BaseSpecification<Cuenta>
    {

        public CuentaByIdSpecification () { }
        public CuentaByIdSpecification(Guid id) : base(x => x.Id == id) { }

        public CuentaByIdSpecification(string numeroCuenta) : base(x => x.NumeroCuenta == numeroCuenta) { }
    }
}
