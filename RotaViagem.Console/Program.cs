using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RotaViagem.Console.Domain;
using RotaViagem.Console.Extension;

namespace RotaViagem.Console {
    class Program {
        static void Main (string[] args) {

            while (true) Executar ();
        }

        static void Executar () {
            try {

                System.Console.WriteLine ("Informe a rota (exemplo GRU-CDG)");
                var rota = System.Console.ReadLine ();

                if (string.IsNullOrEmpty (rota) || !rota.Contains ('-')) {
                    System.Console.WriteLine ("Rota inválida!");
                    return;
                }

                var origem = rota.Split ('-') [0];
                var destino = rota.Split ('-') [1];

                var httpRota = ExecutaHttp ("http://localhost:5000/api/v1/CalculoRota", new { Origem = origem, Destino = destino }).Result;

                var result = JsonConvert.DeserializeObject<Result> (httpRota.Content);

                if (string.IsNullOrEmpty (result.Erro)) {
                    System.Console.WriteLine ($"Melhor rota: {result.MelhorRota}");
                } else {
                    System.Console.WriteLine ($"Ocorreu um erro ao calcular: {result.Erro}");
                }

            } catch (Exception ex) {
                System.Console.WriteLine ($"Algo deu errado! Erro: {ex.Message}");
            }
        }

        static Task<RestResponse> ExecutaHttp (string url, object content) {
            var client = new RestClient (url);
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest (Method.POST);
            request.AddJsonBody (content);
            return client.ExecuteAsync (request);
        }
    }
}