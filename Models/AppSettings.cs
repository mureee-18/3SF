namespace ThreeSF.Models;

/// <summary>
/// settings.json に保存されるアプリ設定。
/// </summary>
public sealed class AppSettings
{
    public string UserName { get; set; } = string.Empty;

    public bool IsDarkTheme { get; set; }

    public bool StartMinimized { get; set; }

    public DateTime? LastSavedAt { get; set; }
}
