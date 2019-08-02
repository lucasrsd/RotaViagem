using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RotaViagem.Console.Domain;

namespace RotaViagem.Console {
    class Program {
        static void Main (string[] args) {

            try {
                var res = ExecutaHttp ("http://localhost:5000/api/v1/CalculoRota", new { Origem = "ABC", Destino = "XYZ" });
            } catch (Exception ex) {
                var erro = ex;
            }
        }

        static Result ExecutaHttp (string url, object content) {
            using (var httpClientHandler = new HttpClientHandler ()) {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient (httpClientHandler)) {
                    client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
                    var res = client.PostAsync (url, new StringContent (content.ToString (), Encoding.UTF32, "application/json")).Result;
                    return JsonConvert.DeserializeObject<Result> (res.Content.ToString ());
                }
            }
        }
    }
}