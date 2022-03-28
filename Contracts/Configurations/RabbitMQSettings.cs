using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Configurations
{
    public class RabbitMQSettings
    {
        public string Host { get; set; } = null!;
        public string? VirtualHost { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
