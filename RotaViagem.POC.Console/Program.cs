// C# program to print all 
// paths from a source to 
// destination. 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// A directed graph using 
// adjacency list representation 
public class Graph {

    // No. of vertices in graph 
    private int v;

    // adjacency list 
    private List<CustoRota>[] adjList;
    private List<List<int>> Rotas;

    // Constructor 
    public Graph (int vertices) {

        // initialise vertex count 
        this.v = vertices;

        this.Rotas = new List<List<int>> ();

        // initialise adjacency list 
        initAdjList ();
    }

    // utility method to initialise 
    // adjacency list 
    private void initAdjList () {
        adjList = new List<CustoRota>[v];

        for (int i = 0; i < v; i++) {
            adjList[i] = new List<CustoRota> ();
        }
    }

    // add edge from u to v 
    public void addEdge (int u, int v, int custo) {
        // Add v to u's list. 
        adjList[u].Add (new CustoRota (v, custo));
    }

    // Prints all paths from 
    // 's' to 'd' 
    public void ExecutarPaths (int s, int d) {
        bool[] isVisited = new bool[v];
        List<int> pathList = new List<int> ();

        // add source to path[] 
        pathList.Add (s);

        // Call recursive utility 
        ExecutarPathsUtil (s, d, isVisited, pathList);
    }

    public int ObterCusto (List<int> routes) {
        // varrer cada posicao e somar ao valor do edge
        var soma = 0;
        for (int x = 0; x < routes.Count; x++) {

            if (x + 1 >= routes.Count) continue;

            var valorRota = adjList[routes[x]].Where (a => a.Destino.Equals (routes[x + 1])).FirstOrDefault ();
            if (valorRota != null) {
                soma += valorRota.Valor;
            }
        }

        return soma;
    }

    public Resultado ObterResultado (Localizacao localizacoes, List<int> rota) {

        var custo = ObterCusto (rota);
        var result = $"Rota ({string.Join(" ", rota)}) -> ";

        foreach (var local in rota) {
            var nomeLocal = localizacoes.ObterPorNome (local);
            result += nomeLocal.Nome + " -> ";
        }

        result += " Custo total: " + custo;

        return new Resultado () {
            Custo = custo,
                Rota = rota,
                Mensagem = result
        };
    }

    // A recursive function to print 
    // all paths from 'u' to 'd'. 
    // isVisited[] keeps track of 
    // vertices in current path. 
    // localPathList<> stores actual 
    // vertices in the current path 
    private void ExecutarPathsUtil (int u, int d,
        bool[] isVisited,
        List<int> localPathList) {

        // Mark the current node 
        isVisited[u] = true;

        if (u.Equals (d)) {
            this.Rotas.Add (new List<int> (localPathList));

            // if match found then no need 
            // to traverse more till depth 
            isVisited[u] = false;
            return;
        }

        // Recur for all the vertices 
        // adjacent to current vertex 
        foreach (int i in adjList[u].Select (x => x.Destino)) {
            if (!isVisited[i]) {
                // store current node 
                // in path[] 
                localPathList.Add (i);
                ExecutarPathsUtil (i, d, isVisited,
                    localPathList);

                // remove current node 
                // in path[] 
                localPathList.Remove (i);
            }
        }

        // Mark the current node 
        isVisited[u] = false;
    }

    // dotnet run "C:\Users\Usuario\Documents\Projetos e Testes Tecnicos\RotaViagem\Csv\input-routes.csv"
    public static void Main (String[] args) {

        var file = args[0];

        if (!File.Exists (file)) {
            Console.WriteLine ("Arquivo não existe. -> " + file);
            return;
        }

        Console.WriteLine ("Iniciando");
        var listaRotas = new List<Conexao> ();

        // Simulando processamento da planilha

        var localizacoes = new Localizacao ();

        var textLines = File.ReadAllLines (file);

        foreach (var item in textLines) {
            var splitedItem = item.Split (';');
            if (splitedItem.Count () < 2) continue;

            var origem = splitedItem[0];
            var destino = splitedItem[1];
            var custoString = splitedItem[2];
            var custoInt = 0;

            if (int.TryParse (custoString, out custoInt)) {
                listaRotas.Add (new Conexao (localizacoes.InstanciaLocal (origem), localizacoes.InstanciaLocal (destino), custoInt));
            }
        }

        // Create a sample graph 
        Graph g = new Graph (localizacoes.indexLocal);

        foreach (var item in listaRotas) {
            g.addEdge (item.Origem.Id, item.Destino.Id, item.Custo);
        }

        while (true) {

            Console.WriteLine ("Informe a rota desejada (De-para, exemplo: GRU-ORL");
            var rotaInput = Console.ReadLine ();

            if (string.IsNullOrEmpty (rotaInput) || !rotaInput.Contains ("-")) {
                Console.WriteLine ("Rota inválida.");
                continue;
            }

            var origem = rotaInput.Split ('-') [0];
            var destino = rotaInput.Split ('-') [1];

            ProcessarRota (g, localizacoes, origem, destino);
        }
    }

    static void ProcessarRota (Graph g, Localizacao localizacoes, string inputOrigem, string inputDestino) {
        var origem = localizacoes.ObterPorId (inputOrigem);
        var destino = localizacoes.ObterPorId (inputDestino);

        if (origem == null) {
            Console.WriteLine ("Origem não encontrada.");
            return;
        }

        if (destino == null) {
            Console.WriteLine ("Destino não encontrado.");
            return;
        }

        Console.WriteLine ("Buscando todas combinações de rotas entre " + origem.Nome + " e " + destino.Nome);
        g.ExecutarPaths (origem.Id, destino.Id);

        List<Resultado> listaResultados = new List<Resultado> ();

        if (g.Rotas.Any ()) {
            Console.WriteLine ("Rotas encontradas: ");

            foreach (var item in g.Rotas) {
                var resultado = g.ObterResultado (localizacoes, item);
                listaResultados.Add (resultado);
                //Console.WriteLine (resultado.Mensagem);
            }

            var rotaComMenorCusto = listaResultados.OrderBy (x => x.Custo).FirstOrDefault ();

            Console.WriteLine ("############ Rota com menor custo ############");
            Console.WriteLine (rotaComMenorCusto.Mensagem);
        } else {
            Console.WriteLine ("Nenhuma rota encontrada.");
        }

        Console.WriteLine ("#######################################");
    }
}