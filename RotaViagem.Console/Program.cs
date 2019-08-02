using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RotaViagem.Console.Domain;
using RotaViagem.Console.Extension;

namespace RotaViagem.Console {
    class Program {

        static IConfiguration BuildConfiguration () {
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ($"appsettings.json", true, true);
            return builder.Build ();
        }

        static void Main (string[] args) {

            var config = BuildConfiguration ();
            var baseUrl = config["BaseUrl"];

            while (true) Executar (baseUrl);
        }

        static void Executar (string baseUrl) {
            try {

                System.Console.WriteLine ("Informe a rota (exemplo GRU-CDG)");
                var rota = System.Console.ReadLine ();

                if (string.IsNullOrEmpty (rota) || !rota.Contains ('-')) {
                    System.Console.WriteLine ("Rota inválida!");
                    return;
                }

                var origem = rota.Split ('-') [0];
                var destino = rota.Split ('-') [1];

                var httpRota = ExecutaHttp ($"{baseUrl}/v1/CalculoRota", new { Origem = origem, Destino = destino }).Result;

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