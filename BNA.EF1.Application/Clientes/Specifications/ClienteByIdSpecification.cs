using BNA.EF1.Application.Common.Specifications.Base;
using BNA.EF1.Domain.Clientes;
using BNA.EF1.Domain.Cliente;

namespace BNA.EF1.Application.Clientes.Specifications
{
    public sealed class ClienteByIdSpecification : BaseSpecification<Cliente>
    {

        public ClienteByIdSpecification(double cuil) : base(x => x.Cuil == cuil) { }

        public ClienteByIdSpecification(){ }
        public ClienteByIdSpecification(Guid id) : base(x => x.Id == id) { }
    }
}
