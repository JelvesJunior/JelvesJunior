using Microsoft.Maui.Controls;
using System;
namespace CreatorApp;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;
    private readonly PlaylistsPage _playlistsPage;

    public LoginPage(LoginViewModel viewModel, PlaylistsPage playlistsPage)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        _playlistsPage = playlistsPage;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var token = await _viewModel.LoginAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            await Navigation.PushAsync(_playlistsPage);
            await _playlistsPage.ViewModel.LoadAsync();
        }
    }
}
