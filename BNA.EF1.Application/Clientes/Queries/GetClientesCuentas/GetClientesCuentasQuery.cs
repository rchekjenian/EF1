using AutoMapper;
using BNA.EF1.Application.Clientes.Queries.GetCliente;
using BNA.EF1.Application.Clientes.Specifications;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Application.Cuentas.Queries.GetCuenta;
using BNA.EF1.Domain.Clientes;
using BNA.EF1.Domain.Cuentas;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Application.Clientes.Queries.GetClientesCuentas
{
    public sealed record GetClientesCuentasQuery(double Cuil):IRequest<ErrorOr<List<GetCuentaDto>>>;
    public sealed class GetClientesCuentasQueryHandler : IRequestHandler<GetClientesCuentasQuery, ErrorOr<List<GetCuentaDto>>>
    {
        private readonly IRepository<Cliente> _repository;
        private readonly IMapper _mapper;

        public GetClientesCuentasQueryHandler(IRepository<Cliente> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<GetCuentaDto>>> Handle(GetClientesCuentasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Cliente = await _repository.GetAsync(new ClienteByCuilSpecification(request.Cuil),cancellationToken ).ConfigureAwait(false);

                if ( Cliente == null) 
                {
                    return Error.NotFound("Cliente.noFound", "No existe un Cliente para el cuil solicitado");
                }

                List<GetCuentaDto> cuentas = _mapper.Map<List<GetCuentaDto>>(Cliente.Cuentas);

                return cuentas;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }













}
