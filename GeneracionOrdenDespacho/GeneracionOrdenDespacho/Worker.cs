using DotPulsar;
using DotPulsar.Extensions;
using DotPulsar.Internal;
using GeneracionOrdenDespacho.ViewModels;
using System.Buffers;
using System.Text;

namespace GeneracionOrdenDespacho
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;

        public Worker(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HelperPulsarBroker.ConsumeMessages<EventoInventarioVerificado>(_configuration);

            while (!stoppingToken.IsCancellationRequested)
            {
                Thread.Sleep(1000);
            }
        }

    }
}