using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RotaViagem.BackEnd.Contexts.Localizacao;
using RotaViagem.BackEnd.Models;
using RotaViagem.BackEnd.Models.CalculadorRota;
using RotaViagem.BackEnd.Services.ArquivoConexao;
using RotaViagem.BackEnd.Services.Custo;
using RotaViagem.BackEnd.Services.Graph;

namespace RotaViagem.BackEnd.Services.CalculadorRota {
    public class CalculadorRotaService : ICalculadorRotaService {

        private ILocalizacaoContext LocalizacaoContext { get; set; }
        private ICustoService CustoService { get; set; }
        private IGraphService GraphService { get; set; }
        private IArquivoConexaoService ArquivoRotaService { get; set; }
        public CalculadorRotaService (ILocalizacaoContext localizacaoContext,
            ICustoService custoService,
            IGraphService graphService,
            IArquivoConexaoService arquivoRotaService) {
            this.LocalizacaoContext = localizacaoContext;
            this.CustoService = custoService;
            this.GraphService = graphService;
            this.ArquivoRotaService = arquivoRotaService;
        }

        public async Task<CalculadorRotaResult> Executar (CalculadorRotaModel model) {

            var conexoes = await this.ArquivoRotaService.CarregarConexoes ();

            var origem = this.LocalizacaoContext.ObterPorId (model.Origem);
            var destino = this.LocalizacaoContext.ObterPorId (model.Destino);

            if (origem == null)
                return new CalculadorRotaResult () { Erro = "Origem não encontrada." };

            if (destino == null)
                return new CalculadorRotaResult () { Erro = "Destino não encontrado." };

            this.GraphService.SetVertices (this.LocalizacaoContext.ObterQuantidadeIndices ());

            foreach (var item in conexoes) {
                this.GraphService.AddEdge (item.Origem.Id, item.Destino.Id, item.Custo);
            }

            var todasRotas = this.GraphService.ExecutarPaths (origem.Id, destino.Id);

            var rotaComMenorCusto = this.CustoService.ObterMenorCusto (todasRotas, this.GraphService.adjList);

            if (rotaComMenorCusto == null)
                return new CalculadorRotaResult () { Erro = "Nenhuma rota localizada." };
            else
                return new CalculadorRotaResult () { MelhorRota = rotaComMenorCusto.Mensagem };
        }

    }
}