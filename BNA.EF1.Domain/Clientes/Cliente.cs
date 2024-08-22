using BNA.EF1.Domain.Common;
using BNA.EF1.Domain.Cliente.Errors;
using ErrorOr;
using BNA.EF1.Domain.Cuentas;
using BNA.EF1.Domain.Example.Errors;
using BNA.EF1.Domain.Example;

namespace BNA.EF1.Domain.Clientes
{
    public sealed class Cliente : Entity
    {
        public Cliente(string nombre, string apellido, double cuil, Guid? id = null) : base(id)
        {
            Nombre = nombre;
            Apellido = apellido;
            Cuil = cuil;
        }


        public static ErrorOr<Cliente> Create(string Nombre,
            string Apellido, double cuil,
            Guid? id = null)
        {
            if (string.IsNullOrEmpty(Nombre))
                return ClienteClassErrors.NullOrEmptyNombre;
            if (string.IsNullOrEmpty(Apellido))
                return ClienteClassErrors.NullOrEmptyApellido;
             if (string.IsNullOrEmpty(cuil.ToString()))
                return ClienteClassErrors.NullOrEmptyCuil;

            return new Cliente(Nombre, Apellido, cuil, id);
        }


        private Cliente() { }

        public string Nombre { get; init;  }
        public string Apellido { get; init; }
        public double Cuil { get; init; }

        public ICollection<BNA.EF1.Domain.Cuentas.Cuenta> Cuentas { get;}
    }
}
