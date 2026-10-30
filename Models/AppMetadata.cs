using System.Reflection;

namespace ThreeSF.Models;

/// <summary>
/// アプリ全体で使う定数・パス・バージョン情報。
/// </summary>
public static class AppMetadata
{
    public const string DisplayName = "3SF";

    public static string Version
    {
        get
        {
            var version = typeof(AppMetadata).Assembly.GetName().Version;
            return version is null ? "0.0.0" : $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }

    /// <summary>
    /// アプリ専用データフォルダー。
    /// Windows は %LOCALAPPDATA%\3SF、それ以外は MAUI の既定フォルダーを使う。
    /// （未パッケージの Windows では MAUI 既定が "User Name" のような不自然な階層になるため）
    /// </summary>
    public static string DataDirectory =>
        OperatingSystem.IsWindows()
            ? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                DisplayName)
            : FileSystem.AppDataDirectory;

    public static string SettingsFilePath => Path.Combine(DataDirectory, "settings.json");
}
