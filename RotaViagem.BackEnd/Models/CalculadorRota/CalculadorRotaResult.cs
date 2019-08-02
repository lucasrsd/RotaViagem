using System.ComponentModel.DataAnnotations;

namespace RotaViagem.BackEnd.Models.CalculadorRota {
    public class CalculadorRotaResult {
        public string MelhorRota { get; set; }
        public string Erro { get; set; }
    }
}