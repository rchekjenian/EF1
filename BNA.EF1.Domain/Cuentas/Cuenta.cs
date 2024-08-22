using BNA.EF1.Domain.Common;
using System.Collections.Generic;

namespace BNA.EF1.Domain.Cuentas
{
    public  class Cuenta : Entity
    {
        public Cuenta(string numeroCuenta, string codigoSucursal,int tipoCuenta, double saldo,
            Guid clienteId, Guid? id = null) : base(id)
        {
            NumeroCuenta = numeroCuenta;
            CodigoSucursal = codigoSucursal;
            TipoCuenta = tipoCuenta;
            Saldo = saldo;
            ClienteId = clienteId;
        }
        public Cuenta(string numeroCuenta, string codigoSucursal, int tipoCuenta, double saldo,
             Guid? id = null) : base(id)
        {
            NumeroCuenta = numeroCuenta;
            CodigoSucursal = codigoSucursal;
            TipoCuenta = tipoCuenta;
            Saldo = saldo;
        }



        private Cuenta() { }

        public string NumeroCuenta { get; init; }
        public string CodigoSucursal { get; init; }
        public double Saldo { get; init; }

        public int TipoCuenta { get; init; }

        public Guid ClienteId { get; init; }



    }
}
