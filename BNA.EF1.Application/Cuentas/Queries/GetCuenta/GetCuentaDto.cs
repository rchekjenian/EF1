using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Application.Cuentas.Queries.GetCuenta
{
    public sealed record GetCuentaDto(Guid Id, string NumeroCuenta, string CodigoSucursal, double Saldo, int TipoCuenta,Guid ClienteId);
    

}
