using ThreeSF.Models;
using ThreeSF.Services;

namespace ThreeSF;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        VersionLabel.Text = $"v{AppMetadata.Version}";
    }

    private void OnToggleThemeClicked(object? sender, EventArgs e) =>
        AppServices.SettingsViewModel.ToggleThemeCommand.Execute(null);
}
