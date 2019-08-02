using System;
using System.Collections.Generic;
using System.Linq;
using RotaViagem.BackEnd.Models;

namespace RotaViagem.BackEnd.Contexts.Localizacao {
    public class LocalizacaoContext : ILocalizacaoContext {
        private List<Local> Locais;
        private int IndexLocal { get; set; }

        public LocalizacaoContext () {
            this.Locais = new List<Local> ();
            this.IndexLocal = 1;
        }

        public int ObterQuantidadeIndices () => this.IndexLocal;

        public Local ObterPorNome (int id) {
            var currentLocal = this.Locais.Where (x => x.Id.Equals (id)).FirstOrDefault ();
            if (currentLocal != null)
                return currentLocal;

            return null;
        }

        public Local ObterPorId (string nome) {
            var currentLocal = this.Locais.Where (x => x.Nome.Equals (nome, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault ();
            if (currentLocal != null)
                return currentLocal;

            return null;
        }

        public Local InstanciaLocal (string local) {
            var currentLocal = this.Locais.Where (x => x.Nome.Equals (local, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault ();

            if (currentLocal != null)
                return currentLocal;
            else {
                currentLocal = new Local (IndexLocal++, local);
                this.Locais.Add (currentLocal);
                return currentLocal;
            }
        }
    }
}