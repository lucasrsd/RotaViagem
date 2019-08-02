using System.Collections.Generic;

namespace RotaViagem.BackEnd.Models {
    public class Resultado {
        public List<int> Rota { get; set; }
        public int Custo { get; set; }
        public string Mensagem { get; set; }
    }
}