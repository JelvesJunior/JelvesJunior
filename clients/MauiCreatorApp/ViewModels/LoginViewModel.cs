using System.Net.Http;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CreatorApp;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _email = string.Empty;
    private string _password = string.Empty;
    private bool _isBusy;

    public event PropertyChangedEventHandler? PropertyChanged;

    public LoginViewModel(HttpClient httpClient)
    {
        _ = httpClient;
    }

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }

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

    public async Task<string?> LoginAsync()
    {
        if (IsBusy)
        {
            return null;
        }

        try
        {
            IsBusy = true;
            // chamada fictícia à API para simplificar o protótipo
            await Task.Delay(500);
            return "fake-jwt-token";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
