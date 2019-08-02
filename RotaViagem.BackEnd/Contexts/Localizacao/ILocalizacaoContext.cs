using RotaViagem.BackEnd.Models;

namespace RotaViagem.BackEnd.Contexts.Localizacao {
    public interface ILocalizacaoContext {
        int ObterQuantidadeIndices ();
        Local ObterPorNome (int id);
        Local ObterPorId (string nome);
        Local InstanciaLocal (string local);
    }
}