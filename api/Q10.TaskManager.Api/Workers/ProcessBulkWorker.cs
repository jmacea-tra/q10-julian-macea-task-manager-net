using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Q10.TaskManager.Infrastructure.DTOs;
using Q10.TaskManager.Infrastructure.Interfaces;

namespace Q10.TaskManager.Api.Workers
{
    public class ProcessBulkWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ProcessBulkWorker> _logger;

        public ProcessBulkWorker(IServiceProvider serviceProvider, ILogger<ProcessBulkWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ProcessBulkWorker started");

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var processBulkService = scope.ServiceProvider.GetRequiredService<IProcessBulkService>();
                    await processBulkService.StartConsumingAsync();

                    //Si llegamos aqui, el servicio se ha iniciado correctamente
                    _logger.LogInformation("ProcessBulkService started successfully.");

                    // Mantener el worker corriendo.
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await Task.Delay(1000, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting ProcessBulkService");
                await Task.Delay(Timeout.Infinite, stoppingToken); // Detener el worker en caso de error
            }

            // Keep the worker running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("ProcessBulkWorker stopped");
        }
    }
}