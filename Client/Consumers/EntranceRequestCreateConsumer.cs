using Contracts;
using MassTransit;

namespace Client.Consumers
{
    public class EntranceRequestCreateConsumer : IConsumer<EntranceRequestCreateMessage>
    {
        private readonly ILogger<EntranceRequestCreateConsumer> _log;

        public EntranceRequestCreateConsumer(ILogger<EntranceRequestCreateConsumer> log)
        {
            _log = log;
        }

        public Task Consume(ConsumeContext<EntranceRequestCreateMessage> context)
        {
            _log.LogInformation($"Created -> {context.Message}");
            return Task.CompletedTask;
        }
    }
}
