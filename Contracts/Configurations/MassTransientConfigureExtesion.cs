using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Configurations
{
    public static class MassTransientConfigureExtesion
    {
        public static void AddMassTransientWithRabbitMQ(this IServiceCollection services, 
            Func<RabbitMQSettings> rabbitSettings,
            Action<IBusRegistrationConfigurator>? configuration = null)
        {
            var settings = rabbitSettings?.Invoke();

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(rabbitSettings));
            }

            services.AddMassTransit(x =>
            {
                configuration?.Invoke(x);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.VirtualHost, conf =>
                    {
                        conf.Username(settings.Username);
                        conf.Password(settings.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
