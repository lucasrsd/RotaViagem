using System;
using System.Collections.Generic;
using System.Linq;
using RotaViagem.BackEnd.Models;

namespace RotaViagem.BackEnd.DB.Rota {
    public class RotaDB : IRotaDB {
        private string ArquivoDb { get; set; }
        public RotaDB (string file) {
            this.ArquivoDb = file;
        }
        public string ObterArquivo () => this.ArquivoDb;
    }
}