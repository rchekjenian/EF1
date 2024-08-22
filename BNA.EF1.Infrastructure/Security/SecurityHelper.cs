using BNA.Security.Lite.Implementations;
using Microsoft.Extensions.Options;

namespace BNA.EF1.Infrastructure.Security
{
    public interface ISecurityHelper
    {
        string ExampleUser { get; }

        string GetExamplePassword();
    }
    public sealed class SecurityHelper : ISecurityHelper
    {
        private readonly IOptionsMonitor<SecurityOptions> _settings;
        private static EncryptedFile _encryptedFile = null!;

        public SecurityHelper(IOptionsMonitor<SecurityOptions> settings)
        {
            _settings = settings;
            CreateEncryptedFile();
        }

        public string ExampleUser { get => _settings.CurrentValue.ExampleUser; }

        public string GetExamplePassword()
        {
            try
            {
                return ObtainKey(_settings.CurrentValue.ExampleUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ObtainKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new NullReferenceException("Error de referencia nula, probablemente el ensamblado proveedor de seguridad no encontró la definición del usuario '"
                                                     + key + "'. Revise la config. de seguridad y/o los nombres de los usuarios en el archivo de conf.");

            return _encryptedFile.Read(key);

        }

        private EncryptedFile CreateEncryptedFile()
        {
            if (_encryptedFile == null)
                _encryptedFile = new EncryptedFile(_settings.CurrentValue);

            return _encryptedFile;
        }
    }
}
