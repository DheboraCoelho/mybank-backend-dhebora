# MyBank Backend - Desafio MyBank

## ✨ Visão Geral

Este repositório representa a minha entrega para o desafio **MyBank**, onde optei por construir uma API utilizando os conceitos de **Clean Architecture** com aplicação básica de **Domain-Driven Design (DDD)**. Apesar das limitações de tempo e experiência, busquei estruturar o projeto de forma escalável, organizada e orientada a boas práticas de arquitetura.

## 🧠 Ideia Inicial

Minha intenção desde o início foi estruturar a aplicação com base na Clean Architecture, organizando o código em camadas separadas por responsabilidades, e aplicar conceitos básicos de Domain-Driven Design, como a separação de domínio, aplicação e infraestrutura.

Além disso, procurei reutilizar algumas partes do código do repositório base luisfabiosm/POC-MyBank, principalmente como referência de comportamento esperado, estrutura de entidades e comandos, mas com adaptações para refletir melhor os princípios de separação de responsabilidades.

## 🏗️ Estrutura do Projeto

Abaixo está a estrutura do projeto com a explicação de cada camada e subpastas importantes:

```
MyBankBackend/
└── src/
    └── MyBankBackend/
        ├── MyBank.Application/
        │   ├── DTOs/
        │   ├── Interfaces/
        │   ├── Services/
        │   └── Class1.cs
        │
        ├── MyBank.Domain/
        │   ├── Account/
        │   │   ├── Entities/
        │   │   ├── Interfaces/
        │   │   └── Services/
        │   ├── Auth/
        │   │   ├── Entities/
        │   │   └── Interfaces/
        │   ├── Notification/
        │   │   └── Entities/
        │   ├── Pix/
        │   │   ├── Entities/
        │   │   └── Interfaces/
        │   └── ValueObjects/
        │
        ├── MyBank.Infrastructure/
        │   ├── Data/
        │   │   ├── Repositories/
        │   │   └── AppDbContext.cs
        │   └── DependencyInjection.cs
        │
        └── MyBank.WebApi/
            ├── Controllers/
            ├── Middlewares/
            └── Program.cs / Startup.cs
```

## 📊 Importância de Cada Camada

### MyBank.Application

* **Responsável por**: Casos de uso e orquestração da lógica de negócio.
* **Contém**: DTOs, Interfaces e Services.
* **Pontos a evoluir**: Faltam classes de UseCases bem definidas.

### MyBank.Domain

* **Responsável por**: O núcleo do negócio (regras, entidades e contratos).
* **Contém**: Entidades, Interfaces, ValueObjects e divisão por contexto (Account, Pix, Auth...)
* **Pontos fortes**: Boa separação por subdomínio.
* **Pontos a evoluir**: Adição de regras de negócio diretamente nas entidades.

### MyBank.Infrastructure

* **Responsável por**: Implementar persistência de dados e injeção de dependência.
* **Contém**: Repositórios, AppDbContext e configuração de DI.

### MyBank.WebApi

* **Responsável por**: Interface com o mundo externo (Controllers, Middlewares).
* **Contém**: Configuração do pipeline, endpoints, autenticação, etc.

## ⚙️ Configuração

* A aplicação utiliza o sistema padrão de configuração do **ASP.NET Core**.
* Arquivo principal: `appsettings.json`.
* Contém as configurações de conexão com banco e JWT.

  
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyBankDB;"
  },
  "Jwt": {
    "Key": "chave_secreta_32_chars",
    "Issuer": "MyBankAPI",
    "Audience": "MyBankUsers"
  }
}

```
## 📃 Documentação da API

* Ainda não implementada.
* Sugestão futura: utilizar **Swagger** para documentação dos endpoints.


## ⛔ O que Faltou para ser um DDD e Clean Architecture Completo

Durante o desenvolvimento, enfrentei limitações de tempo e aprendizado. Algumas coisas importantes que ainda faltam ou podem ser melhoradas segundo os links estudados:
* Ausência de **casos de uso** bem definidos na camada Application.
* Pouca **lógica de negócio dentro das entidades** do Domain.
* Falta de **injeção de dependência invertida** em alguns pontos (D de SOLID).
* Pouco uso de **interfaces desacopladas** entre as camadas.
## ❌ DDD Incompleto
* Ubiquitous Language ainda não está totalmente claro no código.
* Aggregates e Value Objects poderiam estar melhor definidos.
* Faltam Services de Domínio para encapsular regras de negócio complexas.

## ❌ Clean Architecture Incompleta
* Algumas dependências ainda apontam do centro para a borda, violando o conceito de dependência inversa.
* A separação entre camadas poderia ser mais rígida com interfaces bem definidas.
* Algumas lógicas de negócio ainda estão dentro dos Controllers.

## 📚 Fontes consultadas:
* DDD do jeito certo - YouTube
* SOLID e DIP - DTI Digital
* Martin Fowler sobre DDD
* Clean Architecture por Gabriel Lemos
* FullCycle sobre DDD
* FullCycle sobre Clean Architecture


## 📖 Minha Trajetória no Desafio

> “Antes deste desafio, eu não tinha muita familiaridade com APIs, nem com ferramentas como o Visual Studio e o .NET Core. No início, eu não sabia nem como criar um projeto de API, então o processo foi cheio de descobertas e superações.”

## Desafios que enfrentei:
* Ambientação com Visual Studio e .NET.
* Estruturação da Clean Architecture sem orientação direta.
* Compreensão do DDD na prática.
* Configuração do EF Core e bancos de dados.

> “Conforme eu aprendia mais sobre essas arquiteturas, percebi que meu projeto ainda estava distante da estrutura ideal, mas isso também me motivou a seguir aprendendo. Mesmo com dificuldades, me orgulhei muito de ter chegado até aqui com um projeto funcional e coerente.”



## 📄 Referências Utilizadas

* [https://fullcycle.com.br/o-que-e-clean-architecture/](https://fullcycle.com.br/o-que-e-clean-architecture/)
* [https://martinfowler.com/tags/domain%20driven%20design.html](https://martinfowler.com/tags/domain%20driven%20design.html)
* [https://www.dtidigital.com.br/blog/solid-principio-inversao-de-dependencia](https://www.dtidigital.com.br/blog/solid-principio-inversao-de-dependencia)
* Canal YouTube "DDD do jeito certo"
* [https://medium.com/@gabrielfernandeslemos/clean-architecture-uma-abordagem-baseada-em-princ%C3%ADpios-bf9866da1f9c](https://medium.com/@gabrielfernandeslemos/clean-architecture-uma-abordagem-baseada-em-princ%C3%ADpios-bf9866da1f9c)


