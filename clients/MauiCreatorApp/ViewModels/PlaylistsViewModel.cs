using System.Net.Http;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace CreatorApp;

public class PlaylistsViewModel : INotifyPropertyChanged
{
    private readonly HttpClient _httpClient;
    private bool _isBusy;
    private string _newPlaylistTitle = string.Empty;
    private string _newPlaylistDescription = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<PlaylistItem> Playlists { get; } = new();

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy != value)
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }

    public string NewPlaylistTitle
    {
        get => _newPlaylistTitle;
        set
        {
            if (_newPlaylistTitle != value)
            {
                _newPlaylistTitle = value;
                OnPropertyChanged();
            }
        }
    }

    public string NewPlaylistDescription
    {
        get => _newPlaylistDescription;
        set
        {
            if (_newPlaylistDescription != value)
            {
                _newPlaylistDescription = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand RefreshCommand { get; }
    public ICommand CreateCommand { get; }

    public PlaylistsViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
        RefreshCommand = new Command(async () => await LoadAsync());
        CreateCommand = new Command(async () => await CreateAsync());
    }

    public async Task LoadAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            Playlists.Clear();
            // chamada fictícia para fins de protótipo
            await Task.Delay(400);
            Playlists.Add(new PlaylistItem("Onboarding", "Sequência introdutória"));
            Playlists.Add(new PlaylistItem("Série Avançada", "Conteúdos avançados"));
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task CreateAsync()
    {
        if (string.IsNullOrWhiteSpace(NewPlaylistTitle))
        {
            return;
        }

        // chamada fictícia para fins de protótipo
        await Task.Delay(300);
        Playlists.Add(new PlaylistItem(NewPlaylistTitle, NewPlaylistDescription));
        NewPlaylistTitle = string.Empty;
        NewPlaylistDescription = string.Empty;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public record PlaylistItem(string Title, string Description);
}
