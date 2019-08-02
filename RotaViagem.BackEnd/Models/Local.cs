namespace RotaViagem.BackEnd.Models {
    public class Local {
        public Local (int id, string nome) {
            this.Id = id;
            this.Nome = nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}