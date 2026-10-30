using System.Diagnostics;
using ThreeSF.Models;
using ThreeSF.Services;

namespace ThreeSF.ViewModels;

public sealed class SettingsViewModel : ViewModelBase
{
    private readonly SettingsService _settingsService;
    private readonly AppSettings _settings;
    private string _userName;
    private bool _isDarkTheme;
    private bool _startMinimized;
    private bool _isDirty;
    private string _statusMessage = "設定はまだ保存されていません。";

    public SettingsViewModel(SettingsService settingsService, AppSettings settings)
    {
        _settingsService = settingsService;
        _settings = settings;

        _userName = settings.UserName;
        _isDarkTheme = settings.IsDarkTheme;
        _startMinimized = settings.StartMinimized;

        SaveCommand = new RelayCommand(Save);
        ToggleThemeCommand = new RelayCommand(() => IsDarkTheme = !IsDarkTheme);
        OpenDataFolderCommand = new RelayCommand(OpenDataFolder);
    }

    public event Action? Saved;

    public string Title => "設定";

    public string UserName
    {
        get => _userName;
        set
        {
            if (SetProperty(ref _userName, value))
            {
                IsDirty = true;
                OnPropertyChanged(nameof(PreviewName));
            }
        }
    }

    public string PreviewName => string.IsNullOrWhiteSpace(UserName) ? "ゲスト" : UserName.Trim();

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
            {
                AppServices.ApplyTheme(value);
                IsDirty = true;
            }
        }
    }

    public bool StartMinimized
    {
        get => _startMinimized;
        set
        {
            if (SetProperty(ref _startMinimized, value))
            {
                IsDirty = true;
            }
        }
    }

    public bool IsDirty
    {
        get => _isDirty;
        private set => SetProperty(ref _isDirty, value);
    }

    public string SettingsPathLabel => $"保存先: {AppMetadata.SettingsFilePath}";

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public RelayCommand SaveCommand { get; }

    public RelayCommand ToggleThemeCommand { get; }

    public RelayCommand OpenDataFolderCommand { get; }

    private void Save()
    {
        _settings.UserName = UserName.Trim();
        _settings.IsDarkTheme = IsDarkTheme;
        _settings.StartMinimized = StartMinimized;
        _settings.LastSavedAt = DateTime.Now;

        _settingsService.Save(_settings);

        IsDirty = false;
        StatusMessage = "設定を保存しました。";
        Saved?.Invoke();
    }

    private void OpenDataFolder()
    {
        Directory.CreateDirectory(AppMetadata.DataDirectory);
        OpenFolder(AppMetadata.DataDirectory);
    }

    internal static void OpenFolder(string path)
    {
        if (OperatingSystem.IsWindows())
        {
            Process.Start(new ProcessStartInfo("explorer.exe", path) { UseShellExecute = true });
            return;
        }

        _ = Launcher.Default.OpenAsync(new Uri(path));
    }
}
