# TruckGarage

## Dependências do projeto
  
  - Microsoft Dotnet -- version 6.0
  - Microsoft.AspNetCore.Mvc.Core -- version 2.2.5
  - Microsoft.NET.Test.Sdk -- version 17.1.0
  - Microsoft.EntityFrameworkCore -- version 7.0.0
  - Microsoft.EntityFrameworkCore.Design -- version 7.0.0
  - Npgsql.EntityFrameworkCore.PostgreSQL -- version 7.0.0
  - Microsoft.EntityFrameworkCore -- version 7.0.0
  - Microsoft.EntityFrameworkCore.Design -- version 7.0.0
  - Npgsql.EntityFrameworkCore.PostgreSQL -- version 7.0.0
  - Moq -- version 4.18.2
  - xunit -- version 2.4.1
  
## Como executar o projeto

### Pré-requisito: dotnet 6.0+  |  dotnet-ef 6.0+
  
  - Para configurar as dependências e compilar o projeto, basta digitar em sua linha de comando:
  
        cd TruckGarage ;
        dotnet build
      
  - Para configurar a conecção com o serviço de banco (postgresql):
  
      Edite o arquivo src/appsettings.json e modifique o sequinte objeto de configuração, preenchendo os campos em branco: Server, User ID, Database e Password:
      
        "ConnectionStrings": {
          "DefaultConnection" :
          "Server=;Port=5432;User ID=;Database=;Password=;Pooling=true;MinPoolSize=1;MaxPoolSize=20;CommandTimeout=15"
         }
    
       * Deve ser levado em consideração que este projeto foi desenvolvido com uma dependência para o serviço de gerenciamento de bancos de dados Postgresql. Com isso, a configuração desejada para o "ConnectionStrings" deve ser verdadeira para o acesso do serviço postgresql.
       
  - Para gerar migrações e atualizar o banco de dados:
  
    - Para gerar migrações:
    
          dotnet ef migrations add --project src/ <MIGRATION-NAME>
          
    - Para atualizar o banco de dados:
    
          dotnet ef database update --project src/ <MIGRATION-NAME>
       
  - Para rodar o projeto, basta digitar a partir da sua linha de comando:
  
        dotnet run --project src/
        
  - Para rodar os tests, basta digitar a partir da sua linha de comando:
  
        dotnet test
