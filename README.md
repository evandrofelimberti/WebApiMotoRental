# WebApiMotoRental

Este repositório contém uma API RESTful para um sistema de aluguel de motos

O projeto consiste em uma API desenvolvida em C# utilizando .NET Core 

Essa API permite a gestão de clientes, motos e aluguéis através de endpoints HTTP

## Funcionalidades

- [x] Cadastro de usuários
- [x] Autenticação com JWT
- [x] CRUD de motos
- [x] Reservas de motos
- [X] Cadastro de motos utilizando sistema de mensageria

## Tecnologias Utilizadas

- .NET Core
- Entity Framework Core
- PostgresQL
- Swagger (para documentação da API)
- RabbitMQ
- xUnit

## Como Executar

1. Clone o repositório: `git clone https://github.com/evandrofelimberti/WebApiMotoRental.git`
2. Abra a solução no Visual Studio ou Visual Studio Code
3. Configure a conexão com o banco de dados no arquivo `appsettings.json`
4. Execute as migrações para criar o banco de dados: `dotnet ef database update`
5. Execute o projeto: `dotnet run`


---

Feito por Evandro Felimberti
