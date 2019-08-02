namespace RotaViagem.BackEnd.Models {
    public class Conexao {
        public Conexao (Local origem, Local destino, int custo) {
            this.Origem = origem;
            this.Destino = destino;
            this.Custo = custo;
        }
        public Local Origem { get; set; }
        public Local Destino { get; set; }
        public int Custo { get; set; }
    }
}