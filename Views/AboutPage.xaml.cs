using ThreeSF.Services;

namespace ThreeSF.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
        BindingContext = AppServices.About;
    }
}
