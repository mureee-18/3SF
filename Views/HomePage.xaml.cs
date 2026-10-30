using ThreeSF.Services;

namespace ThreeSF.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = AppServices.Home;
    }
}
