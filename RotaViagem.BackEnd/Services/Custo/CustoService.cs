using System.Collections.Generic;
using System.Linq;
using RotaViagem.BackEnd.Contexts.Localizacao;
using RotaViagem.BackEnd.Models;

namespace RotaViagem.BackEnd.Services.Custo {
    public class CustoService : ICustoService {

        private ILocalizacaoContext LocalizacaoContext { get; set; }
        public CustoService (ILocalizacaoContext localizacaoContext) {
            this.LocalizacaoContext = localizacaoContext;
        }

        private int ObterCusto (List<CustoRota>[] adjList, List<int> routes) {
            // varrer cada posicao e somar ao valor do edge
            var soma = 0;
            for (int x = 0; x < routes.Count; x++) {

                if (x + 1 >= routes.Count) continue;

                var valorRota = adjList[routes[x]].Where (a => a.Destino.Equals (routes[x + 1])).FirstOrDefault ();
                if (valorRota != null) {
                    soma += valorRota.Valor;
                }
            }

            return soma;
        }

        public Resultado ObterMenorCusto (List<List<int>> todasRotas, List<CustoRota>[] adjList) {
            Resultado rotaComMenorCusto = null;
            if (todasRotas.Any ()) {
                var listaResultados = new List<Resultado> ();
                foreach (var item in todasRotas) {
                    var result = ObterResultado (item, adjList, "Melhor rota: ");
                    listaResultados.Add (result);
                }
                rotaComMenorCusto = listaResultados.OrderBy (x => x.Custo).FirstOrDefault ();
            }
            return rotaComMenorCusto;
        }

        private Models.Resultado ObterResultado (List<int> rota, List<CustoRota>[] adjList, string resultPrefix) {

            var custo = ObterCusto (adjList, rota);
            var result = resultPrefix;

            foreach (var local in rota) {
                var nomeLocal = this.LocalizacaoContext.ObterPorNome (local);
                result += nomeLocal.Nome + " - ";
            }
            result = result.Trim ().TrimEnd ('-');
            result += " -> : $" + custo;

            return new Models.Resultado () {
                Custo = custo,
                    Rota = rota,
                    Mensagem = result
            };
        }
    }
}