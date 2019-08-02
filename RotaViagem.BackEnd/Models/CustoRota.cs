using System;

namespace RotaViagem.BackEnd.Models {
    public class CustoRota {

        public CustoRota (int destino, int valor) {
            this.Destino = destino;
            this.Valor = valor;
        }
        public int Destino { get; set; }
        public int Valor { get; set; }
    }

}