using System.Diagnostics;
using System.Runtime.InteropServices;
using ThreeSF.Models;

namespace ThreeSF.ViewModels;

public sealed class AboutViewModel : ViewModelBase
{
    private string _statusMessage = string.Empty;

    public AboutViewModel()
    {
        OpenDataFolderCommand = new RelayCommand(() =>
        {
            Directory.CreateDirectory(AppMetadata.DataDirectory);
            SettingsViewModel.OpenFolder(AppMetadata.DataDirectory);
            StatusMessage = "設定フォルダーを開きました。";
        });

        OpenInstallFolderCommand = new RelayCommand(() =>
        {
            SettingsViewModel.OpenFolder(AppContext.BaseDirectory);
            StatusMessage = "インストールフォルダーを開きました。";
        });
    }

    public string Title => "このアプリについて";

    public string Subtitle => "3SF デスクトップアプリのビルド情報です。";

    public string Version => AppMetadata.Version;

    public string Framework => RuntimeInformation.FrameworkDescription;

    public string OperatingSystem => RuntimeInformation.OSDescription;

    public string Architecture => RuntimeInformation.ProcessArchitecture.ToString();

    public string DataPath => AppMetadata.SettingsFilePath;

    public string InstallPath => AppContext.BaseDirectory;

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    public RelayCommand OpenDataFolderCommand { get; }

    public RelayCommand OpenInstallFolderCommand { get; }
}
