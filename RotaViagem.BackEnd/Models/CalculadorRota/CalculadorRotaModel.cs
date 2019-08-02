using System.ComponentModel.DataAnnotations;

namespace RotaViagem.BackEnd.Models.CalculadorRota {
    public class CalculadorRotaModel {

        [Required]
        public string Origem { get; set; }

        [Required]
        public string Destino { get; set; }
    }
}