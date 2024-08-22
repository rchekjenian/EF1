using ErrorOr;

namespace BNA.EF1.Domain.Cliente.Errors
{
    public static class ClienteClassErrors
    {
        public static Error NullOrEmptyNombre = Error.Validation(code: "Cliente.NullOrEmptyNombre",
            description: "The Nombre provided is null or empty");


        public static Error NullOrEmptyApellido = Error.Validation(code: "Cliente.NullOrEmptyApellido",
            description: "The Apellido provided is null or empty");


        public static Error NullOrEmptyCuil = Error.Validation(code: "Cliente.NullOrEmptyCuil",
            description: "The Cuil provided is null or empty");

          public static Error ClienteExistente(double cuil)
        {
          return Error.Validation(
             code: "ClienteExistente",
            description: $"Cliente con CUIL {cuil} ya existe."
           );
     }



    }
}
