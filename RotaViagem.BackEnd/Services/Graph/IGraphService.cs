using System.Collections.Generic;
using RotaViagem.BackEnd.Models;

namespace RotaViagem.BackEnd.Services.Graph {
    public interface IGraphService {
        List<CustoRota>[] adjList { get; set; }
        void SetVertices (int vertices);
        void AddEdge (int u, int v, int custo);
        List<List<int>> ExecutarPaths (int s, int d);
    }
}