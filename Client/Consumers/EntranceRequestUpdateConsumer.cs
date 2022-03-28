using Contracts;
using MassTransit;

namespace Client.Consumers
{
    public class EntranceRequestUpdateConsumer : IConsumer<EntranceRequestUpdateMessage>
    {
        private readonly ILogger<EntranceRequestUpdateConsumer> _log;

        public EntranceRequestUpdateConsumer(ILogger<EntranceRequestUpdateConsumer> log)
        {
            _log = log;
        }

        public Task Consume(ConsumeContext<EntranceRequestUpdateMessage> context)
        {
            _log.LogInformation($"Updated -> {context.Message}");
            return Task.CompletedTask;
        }
    }
}
