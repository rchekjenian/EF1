using BNA.EF1.Application.Common.Specifications.Base;
using BNA.EF1.Domain.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Application.Clientes.Specifications
{
    public sealed class ClienteByCuilSpecification : BaseSpecification<Cliente>
    {
        public ClienteByCuilSpecification(double cuil):base(x => x.Cuil == cuil )
        {
            Includes.Add(x => x.Cuentas);
          
        }
    }
}
