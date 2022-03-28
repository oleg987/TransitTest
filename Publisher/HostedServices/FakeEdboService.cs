using Contracts;
using MassTransit;
using Publisher.Models;
using System.Text;

namespace Publisher.HostedServices
{
    public class FakeEdboService : IHostedService
    {
        private readonly string[] _names = { "John", "Alice", "Ben", "Kate", "Jessy", "Bill", "Sarah", "Franck" };
        private readonly Random _rand = new();

        private readonly List<EntranceRequest> _list = new();
        private readonly ILogger<FakeEdboService> _logger;
        private readonly IBus _bus;

        public FakeEdboService(ILogger<FakeEdboService> logger, IServiceProvider provider)
        {
            _logger = logger;
            _bus = provider.CreateScope().ServiceProvider.GetRequiredService<IBus>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                await DoWork();
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task DoWork()
        {
            if (_rand.Next(100) >= 60)
            {
                var request = new EntranceRequest(_names[_rand.Next(_names.Length)], 1);

                _list.Add(request);

                var msg = new EntranceRequestCreateMessage(request.Id, request.Name, request.Status, request.CreatedAt, request.UpdatedAt);
                await _bus.Publish(msg);
                _logger.LogWarning($"Add: {msg}");
            }

            foreach (var req in _list)
            {
                if (_rand.Next(100) >= 80)
                {
                    req.Status += 1;

                    var msg = new EntranceRequestUpdateMessage(req.Id, req.Name, req.Status, req.CreatedAt, req.UpdatedAt);
                    await _bus.Publish(msg);
                    _logger.LogInformation($"Update: {msg}");
                }
            }

            await Task.Delay(10 * 1000);
        }
    }
}
