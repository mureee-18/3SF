using ThreeSF.Services;

namespace ThreeSF;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        AppServices.Initialize();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell())
        {
            Title = "3SF",
            Width = 1120,
            Height = 720,
        };
    }
}
