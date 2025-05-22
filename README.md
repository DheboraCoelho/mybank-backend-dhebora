# MyBank Backend - Desafio MyBank

## ✨ Visão Geral

Este repositório representa a minha entrega para o desafio **MyBank**, onde optei por construir uma API utilizando os conceitos de **Clean Architecture** com aplicação básica de **Domain-Driven Design (DDD)**. Apesar das limitações de tempo e experiência, busquei estruturar o projeto de forma escalável, organizada e orientada a boas práticas de arquitetura.

## ⚖️ Objetivo Inicial

A ideia inicial era:

* Utilizar uma arquitetura **Clean** com separação clara de responsabilidades.
* Aplicar conceitos básicos de **DDD**, como Entities, Value Objects e Interfaces.
* Reaproveitar trechos e ideias do repositório original do desafio: [`luisfabiosm/POC-MyBank`](https://github.com/luisfabiosm/POC-MyBank)
* Aplicar princípios **SOLID**, principalmente a **Inversão de Dependência**.

## 🌐 Estrutura do Projeto

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

## ❌ O que faltou para ser um DDD/Clean completo?

* Ausência de **casos de uso** bem definidos na camada Application.
* Pouca **lógica de negócio dentro das entidades** do Domain.
* Falta de **injeção de dependência invertida** em alguns pontos (D de SOLID).
* Pouco uso de **interfaces desacopladas** entre as camadas.

## 🚀 Minha Trajetória no Desafio

> “Comecei sem saber direito o que era uma API. Nunca tinha trabalhado no Visual Studio com projetos em .NET. O desafio de estrutura e arquitetura foi grande. Conforme fui aprendendo sobre Clean Architecture e DDD, percebi que minha implementação estava distante desses padrões, mas isso não me impediu de continuar e terminar. Foi um processo de muito aprendizado.”

## ⚙️ Configuração

* A aplicação utiliza o sistema padrão de configuração do **ASP.NET Core**.
* Arquivo principal: `appsettings.json`.
* Contém as configurações de conexão com banco e JWT.

## 📃 Documentação da API

* Ainda não implementada.
* Sugestão futura: utilizar **Swagger** para documentação dos endpoints.

## 📄 Referências Utilizadas

* [https://fullcycle.com.br/o-que-e-clean-architecture/](https://fullcycle.com.br/o-que-e-clean-architecture/)
* [https://martinfowler.com/tags/domain%20driven%20design.html](https://martinfowler.com/tags/domain%20driven%20design.html)
* [https://www.dtidigital.com.br/blog/solid-principio-inversao-de-dependencia](https://www.dtidigital.com.br/blog/solid-principio-inversao-de-dependencia)
* Canal YouTube "DDD do jeito certo"
* [https://medium.com/@gabrielfernandeslemos/clean-architecture-uma-abordagem-baseada-em-princ%C3%ADpios-bf9866da1f9c](https://medium.com/@gabrielfernandeslemos/clean-architecture-uma-abordagem-baseada-em-princ%C3%ADpios-bf9866da1f9c)


