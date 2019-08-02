# RotaViagem

## • Como executar a aplicação

### Executar API:

Iniciar a aplicação RotaViagem.BackEnd passando por parâmetro no dotnet run a planilha que será o banco de dados. Exemplo :

dotnet run "C:\\Users\\Usuario\\Documents\\Projetos e Testes Tecnicos\\RotaViagem\\Csv\\input-routes.csv"

### Executar Console:

Iniciar a aplicação console RotaViagem.Console

Atentar-se ao config com a BaseURL da API.

## • Estrutura dos arquivos/pacotes

• ArquivosDesafio: Arquivos recebidos para elaboração do desafio

• Csv: Arquivo csv com base de conexões entre viagens

• RotaViagem.BackEnd: API para consulta de rotas entre viagens e serviços relacionados ao arquivo de base de dados (csv), exemplo: incluir uma nova conexão

• RotaViagem.BackEnd.Tests: Testes unitários

• RotaViagem.Console: Console para interface de consulta de rota mais barata (Executa a API)

• RotaViagem.POC.Console: POC executada antes do teste para testar algorítimos e viabilizar o desenvolvimento


## • Explique as decisões de design adotadas para a solução

Tomei a decisão de seguir o padrão de mercado que utilizamos, tendo o BackEnd separado em APIs / Serviços, e os consoles ou demais canais consumindo os mesmos, mantendo cada um com suas responsabilidades e evitando a redundância de regras / processos.

Mantive meu código o mais limpo possível para a execução do teste, sem criar complexibilidades desnecessárias exclusivamente para o desafio.

## • Descreva sua API Rest de forma simplificada.

A API recebe dois locais para executar o cálculo de todas as possibilidades, buscando o menor custo caso seja encontrado.

A mesma primeiro encontra todas as possibilidades, em sequência faz o calculo da soma de valores dos caminhos localizados.

Algorítimos de modelo utilizados: BFS e Dijkstra