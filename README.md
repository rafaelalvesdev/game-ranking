## Game.Ranking API


#### Dependências
* .NET Core 2.1 SDK (v2.1.302) ou superior - [Download](https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.302-windows-x64-installer ".NET Core 2.1 SDK")
* .NET Core 2.1 Runtime (v2.1.2) ou superior - [Download](https://www.microsoft.com/net/download/thank-you/dotnet-runtime-2.1.2-windows-hosting-bundle-installer ".NET Core 2.1 Runtime")
* ElasticSearch - [Download](https://www.elastic.co/downloads/elasticsearch "Download ElasticSearch")
    -- Para utilizar o ElasticSearch é necessário ter o JDK instalado e o Environment Variable JAVA_HOME configurado. - [Download](http://www.oracle.com/technetwork/pt/java/javase/downloads/jdk8-downloads-2133151.html "JDK 8") - [Configurar JAVA_HOME](https://docs.oracle.com/cd/E19182-01/820-7851/inst_cli_jdk_javahome_t/ "Configurar JAVA_HOME")

Para executar o projeto em DEBUG, utilizar Visual Studio 15.7 ou superior.


#### Componentes utilizados
* Nest (High level ElasticSearch client)
* Entity Framework Core (InMemory Database)
* Hangfire (Job scheduler)
* Swashbuckle (API Swagger generator)


#### Configurações 
1. Configurar a URL de conexão com o ElasticSearch em `code/Game.Ranking.Web/Properties/appsettings.json`, na propriedade `ConnectionStrings / ElasticSearch`.
    -- Por padrão é utilizado http://localhost:9200/
2. Iniciar ElasticSearch 
    -- Pode ser executado diretamente através do bat em `elasticsearch/bin/elasticsearch.bat`.
3. Iniciar a aplicação (Config DEBUG ou RELEASE)


#### Recursos
Na API estão disponíveis 3 endpoints:
1. Inserção de dados de GameResult `POST` `/game-results`.
    -- Os dados recebidos são validados e armazenados em um InMemory database.
2. Replicação `POST` `/game-results/replicate` 
    -- Os dados recebidos capturados da memória e enviados ao ElasticSearch.
    -- Essa rotina roda automaticamente a cada 5 minutos, porém o sistema disponibiliza um endpoint para forçar a replicação.
3. Recuperar Leaderboard (ranking) `GET` `/leaderboard`
    -- Os dados são agregados e recuperados do ElasticSearch.
    -- Estes dados ficam em cache de memória por 1 minuto, após este tempo caso ocorra uma requisição na API são novamente recuperados do banco / cacheados.
    -- É possível enviar o parâmetro top na querystring para definir o número de jogadores que será retornado, o valor deve estar entre 1 e 1000. Caso não seja enviado será utilizado o default de top 100 jogadores.


#### Utilização
* Caso o projeto seja executado em modo DEBUG a API pode ser utilizada através da URL [http://localhost:51334/](http://localhost:51334/).
* A API pode ser acessada e utilizada pelo Swagger, em [http://localhost:51334/swagger](http://localhost:51334/swagger).