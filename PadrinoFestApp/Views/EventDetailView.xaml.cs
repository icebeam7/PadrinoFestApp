using PadrinoFestApp.ViewModels;

namespace PadrinoFestApp.Views;

public partial class EventDetailView : ContentPage
{
	public EventDetailView(EventDetailViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;
    }
}