# MAUI Creator App (Prototype)

Este diretório contém um protótipo didático de um app .NET MAUI para criadores. O objetivo é demonstrar o fluxo de login (fictício) e listagem/edição de playlists consumindo a API.

## Estrutura
- `App.xaml` / `App.xaml.cs`: configura navegação básica.
- `MauiProgram.cs`: registra serviços e `HttpClient` apontando para `http://localhost:5100`.
- `Views/LoginPage.xaml`: primeira tela com formulário simples e botão para navegar para `PlaylistsPage`.
- `Views/PlaylistsPage.xaml`: exibe playlists existentes e permite criar novas.
- `ViewModels`: contém os `ObservableObject` usados nas telas.

## Execução
O projeto não foi criado com o template oficial (`dotnet new maui`), mas os arquivos aqui servem como referência visual. Para torná-lo executável:
1. Crie um projeto MAUI (`dotnet new maui -n CreatorApp`).
2. Copie os arquivos de `Views/` e `ViewModels/` para o projeto criado.
3. Ajuste o namespace conforme necessário e configure os serviços no `MauiProgram.cs`.
4. Execute `dotnet build`/`dotnet maui run` para obter as telas abaixo.

## Wireframes (prints sugeridos)
- **Login**: campos de e-mail/senha e botão "Entrar".
- **Minhas Playlists**: lista com cards e botão "Nova Playlist" que abre um `Popup` simples.

As strings e ícones foram mantidos simples para facilitar a leitura.
