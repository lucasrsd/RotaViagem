using System.Collections.Generic;
using System.Threading.Tasks;
using RotaViagem.BackEnd.Models;
using RotaViagem.BackEnd.Models.ArquivoConexao;

namespace RotaViagem.BackEnd.Services.ArquivoConexao {
    public interface IArquivoConexaoService {
        Task<List<Conexao>> CarregarConexoes ( );
        Task InserirConexao (ArquivoConexaoModel model);
    }
}