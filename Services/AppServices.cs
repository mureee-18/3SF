using ThreeSF.Models;
using ThreeSF.ViewModels;

namespace ThreeSF.Services;

/// <summary>
/// 画面から使うサービスと ViewModel を 1 か所で組み立てる（簡易 DI）。
/// App の起動時に Initialize を 1 回だけ呼ぶ。
/// </summary>
public static class AppServices
{
    public static SettingsService SettingsService { get; } = new();

    public static AppSettings Settings { get; private set; } = new();

    public static HomeViewModel Home { get; private set; } = null!;

    public static SettingsViewModel SettingsViewModel { get; private set; } = null!;

    public static AboutViewModel About { get; private set; } = null!;

    public static void Initialize()
    {
        Settings = SettingsService.Load();
        ApplyTheme(Settings.IsDarkTheme);

        Home = new HomeViewModel(Settings);
        SettingsViewModel = new SettingsViewModel(SettingsService, Settings);
        About = new AboutViewModel();

        SettingsViewModel.Saved += Home.RefreshGreeting;
    }

    public static void ApplyTheme(bool useDarkTheme)
    {
        if (Application.Current is { } application)
        {
            application.UserAppTheme = useDarkTheme ? AppTheme.Dark : AppTheme.Light;
        }
    }
}
