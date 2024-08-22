using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Application.Clientes.Queries.GetCliente
{
    public sealed record GetClienteDto(Guid Id, string Nombre, string Apellido, double Cuil)
    {
    }
}
