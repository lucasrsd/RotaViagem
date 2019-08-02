using System.ComponentModel.DataAnnotations;

namespace RotaViagem.BackEnd.Models.ArquivoConexao {
    public class ArquivoConexaoModel {

        [Required]
        public string Origem { get; set; }

        [Required]
        public string Destino { get; set; }

        [Required]
        public int Custo { get; set; }
    }
}