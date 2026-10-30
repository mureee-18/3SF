using ThreeSF.Services;

namespace ThreeSF.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = AppServices.SettingsViewModel;
    }
}
