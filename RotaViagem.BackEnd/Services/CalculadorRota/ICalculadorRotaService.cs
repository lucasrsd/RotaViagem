using System.Collections.Generic;
using System.Threading.Tasks;
using RotaViagem.BackEnd.Models.CalculadorRota;

namespace RotaViagem.BackEnd.Services.CalculadorRota {
    public interface ICalculadorRotaService {
        Task<CalculadorRotaResult> Executar (CalculadorRotaModel model);
    }
}