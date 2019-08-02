using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RotaViagem.BackEnd.DB.Rota;

namespace RotaViagem.BackEnd {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ()
                .AddNewtonsoftJson ();

            var args = Environment.GetCommandLineArgs ();

            if (!args.Any ())
                throw new ArgumentNullException ("Informe uma planilha para iniciar a API!");

            // Args[0] = DLL do projeto
            // Args[1] = Arquivo CSV - Pode ser setado no ~/.vscode/launch.json (Parametro args), ou no dotnet run "{nome_do_csv.csv}"
            var arquivo = args[1];

            // Acredito que existam formas melhores de validar o arquivo na inicialização do projeto.
            // Como não é comum utilizar arquivos diretamente vou lançar uma exceção ao tentar iniciar.
            
            if (!File.Exists (arquivo))
                throw new ArgumentException ("O arquivo de inicalização da API não existe.");

            services.AddSingleton<IRotaDB> (new RotaDB (arquivo));

            services.AddServices (typeof (Startup).Assembly);
            services.AddContexts (typeof (Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting (routes => {
                routes.MapControllers ();
            });
        }
    }
}