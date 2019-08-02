using System.Collections.Generic;
using System.Linq;
using RotaViagem.BackEnd.Models;

// Referencia : https://www.geeksforgeeks.org/find-paths-given-source-destination/

namespace RotaViagem.BackEnd.Services.Graph {
    public class GraphService : IGraphService {
        private int v;
        public List<CustoRota>[] adjList { get; set; }
        private List<List<int>> Rotas;

        public void SetVertices (int vertices) {
            this.Rotas = new List<List<int>> ();
            this.v = vertices;
            initAdjList ();
        }

        private void initAdjList () {
            adjList = new List<CustoRota>[v];

            for (int i = 0; i < v; i++) {
                adjList[i] = new List<CustoRota> ();
            }
        }

        public void AddEdge (int u, int v, int custo) {
            // Add v to u's list. 
            adjList[u].Add (new CustoRota (v, custo));
        }

        public List<List<int>> ExecutarPaths (int s, int d) {
            bool[] isVisited = new bool[v];
            List<int> pathList = new List<int> ();

            // add source to path[] 
            pathList.Add (s);

            // Call recursive utility 
            ExecutarPathsUtil (s, d, isVisited, pathList);
            return this.Rotas;
        }
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
    }

}