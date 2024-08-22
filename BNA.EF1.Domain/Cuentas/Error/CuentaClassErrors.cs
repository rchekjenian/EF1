using ErrorOr;

namespace BNA.EF1.Domain.Cuentas.Errors
{
    public static class CuentaClassErrors
    {
        public static Error NullOrEmptyNumeroCuenta = Error.Validation(code: "Cuenta.NullOrEmptyNumeroCuenta",
            description: "The NumeroCuenta provided is null or empty");

        public static Error NullOrEmptyCodigoSucursal = Error.Validation(code: "Cuenta.NullOrEmptyCodigoSucursal",
            description: "The CodigoSucursal provided is null or empty");

        public static Error NullOrEmptyTipoCuenta = Error.Validation(code: "Cuenta.NullOrEmptyTipoCuenta",
                    description: "The TipoCuenta provided is null or empty");

        public static Error NullOrEmptySaldo = Error.Validation(code: "Cuenta.NullOrEmptySaldo",
                    description: "The Saldo provided is null or empty");

        public static Error NullOrEmptyClienteId = Error.Validation(code: "Cuenta.NullOrEmptyClienteId",
                   description: "The ClienteId provided is null or empty");

   
    }
}

