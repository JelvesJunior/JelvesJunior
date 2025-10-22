using Microsoft.Maui.Controls;

namespace CreatorApp;

public partial class PlaylistsPage : ContentPage
{
    public PlaylistsViewModel ViewModel { get; }

    public PlaylistsPage(PlaylistsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }
}
