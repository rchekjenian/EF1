using AutoMapper;
using BNA.EF1.Application.Example.Queries.GetExample;
using BNA.EF1.Application.Cuentas.Queries.GetCuenta;

using BNA.EF1.Domain.Example;
using BNA.EF1.Domain.Clientes;

using BNA.EF1.Application.Clientes.Queries.GetCliente;
using BNA.EF1.Domain.Cuentas;

namespace BNA.EF1.Application.Common.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<User, UserValidationResponse>().ReverseMap(); --> Common mapping example

            //CreateMap<Consent, GetConsentResponse>()
            //     .ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
            //     .ForMember(x => x.Cuil, o => o.MapFrom(x => x.User.Cuil)); --> Custom mapping example



            CreateMap<Cliente, Clientes.Queries.GetCliente.GetClientesQuery>();

            CreateMap<Cliente, Clientes.Queries.GetCliente.GetClienteDto>();
            CreateMap<Cuenta, Cuentas.Queries.GetCuenta.GetCuentaDto>().ReverseMap();
        }

    }
}
