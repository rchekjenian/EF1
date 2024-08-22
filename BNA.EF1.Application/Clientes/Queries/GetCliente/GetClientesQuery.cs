using AutoMapper;
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

namespace BNA.EF1.Application.Clientes.Queries.GetCliente

{

    public sealed record GetClientesQuery : IRequest<List<GetClienteDto>>;
    public sealed class GetClientesQueryHandler : IRequestHandler<GetClientesQuery, List<GetClienteDto>>
    {
        private readonly IRepository<Cliente> _repository;
        private readonly IMapper _mapper;

        public GetClientesQueryHandler(IRepository<Cliente> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetClienteDto>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
        {
            var Cliente = await _repository.GetAllAsync(cancellationToken: cancellationToken).ConfigureAwait(false);


            List<GetClienteDto> Clientes = _mapper.Map<List<GetClienteDto>>(Cliente);

            return Clientes;


        }
    }













}
