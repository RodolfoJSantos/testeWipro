using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Wipro.Servico;
using WiproTeste.Models;

namespace WiproTeste.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoedaController : Controller
    {
        private ILogger<MoedaController> _logger;

        private Emissor svc = new Emissor();

        public MoedaController(ILogger<MoedaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(List<Moeda> moedas)
        {
           string retornoSvc = svc.AddItemFila(moedas);

            return Ok(retornoSvc);
        }
    }
}
