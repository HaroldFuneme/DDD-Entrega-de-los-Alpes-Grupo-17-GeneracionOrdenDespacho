using GeneracionOrdenDespacho.ViewModels;

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
            HelperPulsarBroker.ConsumeMessages<EventoInventarioVerificado>(_configuration, _configuration["Pulsar:topico-verifica-inventario"]);
            HelperPulsarBroker.ConsumeMessages<EventoInventarioVerificadoCompensacion>(_configuration, _configuration["Pulsar:topico-verifica-inventario-compensacion"]);
            HelperPulsarBroker.ConsumeMessages<EventoOrden>(_configuration, _configuration["Pulsar:topico-evento-orden-a"]);
            HelperPulsarBroker.ConsumeMessages<EventoOrdenCompensacion>(_configuration, _configuration["Pulsar:topico-evento-orden-a-compensacion"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                Thread.Sleep(1000);
            }
        }

    }
}