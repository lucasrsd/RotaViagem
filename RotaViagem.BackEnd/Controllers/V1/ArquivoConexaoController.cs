using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RotaViagem.BackEnd.Models.ArquivoConexao;
using RotaViagem.BackEnd.Services.ArquivoConexao;

namespace RotaViagem.BackEnd.Controllers.V1 {
    [Route ("api/v1/[controller]")]
    [ApiController]
    public class ArquivoConexaoController : ControllerBase {
        private IArquivoConexaoService ArquivoConexaoService { get; set; }
        public ArquivoConexaoController (IArquivoConexaoService arquivoConexaoService) {
            this.ArquivoConexaoService = arquivoConexaoService;
        }

        [HttpPut]
        public async Task<IActionResult> Incluir ([FromBody] ArquivoConexaoModel model) {
            await this.ArquivoConexaoService.InserirConexao(model);
            return Ok ();
        }
    }
}