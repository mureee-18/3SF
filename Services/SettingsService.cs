using System.Text.Json;
using ThreeSF.Models;

namespace ThreeSF.Services;

/// <summary>
/// 設定ファイルの読み書き。壊れたファイルは既定値にフォールバックする。
/// </summary>
public sealed class SettingsService
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
    };

    public AppSettings Load()
    {
        try
        {
            var path = AppMetadata.SettingsFilePath;
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var settings = JsonSerializer.Deserialize<AppSettings>(json, SerializerOptions);
                if (settings is not null)
                {
                    return settings;
                }
            }
        }
        catch (Exception)
        {
            // 読み込めない設定は無視して既定値を使う（次回保存時に上書きされる）。
        }

        return new AppSettings();
    }

    public void Save(AppSettings settings)
    {
        Directory.CreateDirectory(AppMetadata.DataDirectory);
        File.WriteAllText(AppMetadata.SettingsFilePath, JsonSerializer.Serialize(settings, SerializerOptions));
    }
}
