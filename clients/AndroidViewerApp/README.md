# Android Viewer App (Prototype)

Protótipo em Java/Android para visualizar playlists e detalhes de conteúdo usando Retrofit.

## Estrutura
- `MainActivity.java`: lista playlists em um `RecyclerView`.
- `PlaylistDetailActivity.java`: mostra itens da playlist selecionada.
- `PlaylistService.java`: interface Retrofit apontando para `http://10.0.2.2:5100`.
- `models/`: classes simples para desserialização.
- `layout/`: arquivos XML com as telas de lista e detalhes.

## Como usar
1. Crie um projeto Android vazio (API 24+).
2. Copie as classes e layouts para os diretórios equivalentes.
3. Inclua dependências no `build.gradle`:
   ```gradle
   implementation "com.squareup.retrofit2:retrofit:2.9.0"
   implementation "com.squareup.retrofit2:converter-gson:2.9.0"
   implementation "androidx.recyclerview:recyclerview:1.3.0"
   ```
4. Ajuste o `AndroidManifest` para registrar a `PlaylistDetailActivity`.

As chamadas de rede utilizam `CoroutineScope` simplificado via `Executor` apenas para fins de demonstração. Para produção recomenda-se usar `LiveData`/`ViewModel` ou `Kotlin`.
