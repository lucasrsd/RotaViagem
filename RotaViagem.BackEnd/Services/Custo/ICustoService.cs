using System.Collections.Generic;
using RotaViagem.BackEnd.Models;

namespace RotaViagem.BackEnd.Services.Custo {
    public interface ICustoService {
        Resultado ObterMenorCusto (List<List<int>> todasRotas, List<CustoRota>[] adjList);
    }
}