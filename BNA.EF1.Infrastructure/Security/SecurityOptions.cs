using BNA.Security.Lite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Infrastructure.Security
{
    public sealed class SecurityOptions : ISettings
    {
        public string ConfigPath { get; init; } = string.Empty;

        public string ExampleUser { get; init; } = string.Empty;

        
    }
}
