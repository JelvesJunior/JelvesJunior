using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
namespace CreatorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5100")
        });
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<PlaylistsViewModel>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<PlaylistsPage>();

        return builder.Build();
    }
}
