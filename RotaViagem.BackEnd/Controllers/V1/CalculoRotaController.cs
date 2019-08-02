using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaViagem.BackEnd.Models.CalculadorRota;
using RotaViagem.BackEnd.Services.CalculadorRota;

namespace RotaViagem.BackEnd.Controllers.V1 {
    [Route ("api/v1/[controller]")]
    [ApiController]
    public class CalculoRotaController : ControllerBase {
        private ICalculadorRotaService CalculadorRotaService { get; set; }
        public CalculoRotaController (ICalculadorRotaService calculadorRotaService) {
            this.CalculadorRotaService = calculadorRotaService;
        }

        [HttpPost]
        public async Task<IActionResult> Calcular ([FromBody] CalculadorRotaModel model) {
            var result = await this.CalculadorRotaService.Executar (model);
            return Ok (result);
        }
    }
}