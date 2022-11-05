using PadrinoFestApp.Services;
using PadrinoFestApp.ViewModels;
using PadrinoFestApp.Views;
using System.Windows.Input;

namespace PadrinoFestApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(Helpers.Constants.SpeakerDetailsRoute, typeof(SpeakerDetailView));
        Routing.RegisterRoute(Helpers.Constants.EventDetailsRoute, typeof(EventDetailView));
        BindingContext = this;
    }
}
