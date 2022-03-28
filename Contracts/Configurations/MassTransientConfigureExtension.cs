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
    public static class MassTransientConfigureExtension
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
                    Action<IRabbitMqHostConfigurator> action = (c) => 
                    {
                        c.Username(settings.Username);
                        c.Password(settings.Password);
                    };

                    if (string.IsNullOrWhiteSpace(settings.VirtualHost))
                    {
                        cfg.Host(settings.Host, conf => action?.Invoke(conf));
                    }
                    else
                    {
                        cfg.Host(settings.Host, settings.VirtualHost, conf => action?.Invoke(conf));
                    }

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
