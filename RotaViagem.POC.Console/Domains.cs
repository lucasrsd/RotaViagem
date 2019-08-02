using System;
using System.Collections.Generic;
using System.Linq;

public class CustoRota {

    public CustoRota (int destino, int valor) {
        this.Destino = destino;
        this.Valor = valor;
    }
    public int Destino { get; set; }
    public int Valor { get; set; }
}

public class Local {
    public Local (int id, string nome) {
        this.Id = id;
        this.Nome = nome;
    }
    public int Id { get; set; }
    public string Nome { get; set; }
}

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

public class Resultado {
    public List<int> Rota { get; set; }
    public int Custo { get; set; }
    public string Mensagem { get; set; }

}

public class Localizacao {
    private List<Local> Locais;
    public int indexLocal;

    public Localizacao () {
        this.Locais = new List<Local> ();
        this.indexLocal = 1;
    }

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
            currentLocal = new Local (indexLocal++, local);
            this.Locais.Add (currentLocal);
            Console.WriteLine ($"Local adicionado: Id {currentLocal.Id}  Local {currentLocal.Nome} ");
            return currentLocal;
        }
    }
}