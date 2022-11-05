using PadrinoFestApp.ViewModels;

namespace PadrinoFestApp.Views;

public partial class SpeakerDetailView : ContentPage
{
    public SpeakerDetailView(SpeakerDetailViewModel vm)
    {
        InitializeComponent();

        this.BindingContext = vm;
    }
}