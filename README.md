# Streaming Platform MVP

Este repositório contém uma solução didática que atende ao escopo proposto:

- **API ASP.NET Core** (`src/Api`) com Entity Framework Core e autenticação JWT.
- **Bibliotecas de apoio** (`Domain`, `Application`, `Infrastructure`) usando Repository Pattern.
- **Protótipo .NET MAUI** (`clients/MauiCreatorApp`) mostrando fluxo Login → Minhas Playlists.
- **Protótipo Android (Java)** (`clients/AndroidViewerApp`) consumindo a API via Retrofit.

## Executando a API
1. Instale o [.NET SDK 8.0](https://dotnet.microsoft.com/download).
2. Restaure os pacotes: `dotnet restore StreamingSolution.sln`.
3. Execute as migrações automáticas e rode a API:
   ```bash
   dotnet run --project src/Api/Api.csproj --urls "http://localhost:5100"
   ```
4. Acesse o Swagger em `http://localhost:5100/swagger`.

A base de dados SQLite (`streaming.db`) será criada automaticamente.

## Protótipo MAUI
Os arquivos em `clients/MauiCreatorApp` podem ser copiados para um projeto MAUI criado via `dotnet new maui`. O `LoginPage` navega para `PlaylistsPage`, carregando dados fictícios e permitindo criar playlists locais.

## Protótipo Android
O diretório `clients/AndroidViewerApp` inclui Activities, adapters, modelos e layouts. Copie-os para um projeto Android no Android Studio, configure as dependências listadas no `README.md` local e ajuste o `AndroidManifest`.

## Estrutura
```
StreamingSolution.sln
├── src
│   ├── Api
│   ├── Application
│   ├── Domain
│   └── Infrastructure
└── clients
    ├── MauiCreatorApp
    └── AndroidViewerApp
```
