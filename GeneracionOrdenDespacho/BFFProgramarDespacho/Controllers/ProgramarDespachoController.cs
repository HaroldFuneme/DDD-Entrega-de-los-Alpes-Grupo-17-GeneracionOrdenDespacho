using BFFProgramarDespacho.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BFFProgramarDespacho.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramarDespachoController : ControllerBase
    {
        private readonly ILogger<ProgramarDespachoController> _logger;
        private readonly IConfiguration _configuration;

        public ProgramarDespachoController(ILogger<ProgramarDespachoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }
        [HttpPost]
        public async Task<string> Post([FromBody] EventoInventarioVerificado comando)
        {
            var messageId = await HelperPulsarBroker.SendMessage(_configuration["Pulsar:Uri"], _configuration["Pulsar:TopicoOrdenesDespacho"], _configuration["Pulsar:Subscription"], comando);

            return messageId;
        }
    }
}