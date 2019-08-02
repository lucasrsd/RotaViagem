using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RotaViagem.BackEnd.Contexts.Localizacao;
using RotaViagem.BackEnd.DB.Rota;
using RotaViagem.BackEnd.Models;
using RotaViagem.BackEnd.Models.ArquivoConexao;

namespace RotaViagem.BackEnd.Services.ArquivoConexao {
    public class ArquivoConexaoService : IArquivoConexaoService {

        private ILocalizacaoContext LocalizacaoContext { get; set; }
        private IRotaDB RotaDB { get; set; }

        public ArquivoConexaoService (ILocalizacaoContext localizacaoContext,
            IRotaDB rotaDb) {
            this.LocalizacaoContext = localizacaoContext;
            this.RotaDB = rotaDb;
        }

        public async Task<List<Conexao>> CarregarConexoes () {
            var listaRotas = new List<Conexao> ();

            var textLines = await File.ReadAllLinesAsync (this.RotaDB.ObterArquivo ());

            foreach (var item in textLines) {
                var splitedItem = item.Split (';');
                if (splitedItem.Count () < 2) continue;

                var origem = splitedItem[0];
                var destino = splitedItem[1];
                var custoString = splitedItem[2];
                var custoInt = 0;

                if (int.TryParse (custoString, out custoInt)) {
                    listaRotas.Add (new Conexao (this.LocalizacaoContext.InstanciaLocal (origem), this.LocalizacaoContext.InstanciaLocal (destino), custoInt));
                }
            }

            return listaRotas;
        }

        public Task InserirConexao (ArquivoConexaoModel model) {
            return File.AppendAllLinesAsync (this.RotaDB.ObterArquivo (), new string[] { $"{model.Origem};{model.Destino};{model.Custo}" });
        }
    }
}