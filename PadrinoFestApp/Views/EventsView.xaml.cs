using PadrinoFestApp.ViewModels;

namespace PadrinoFestApp.Views;

public partial class EventsView : ContentPage
{
    public EventsView(EventsViewModel vm)
    {
        InitializeComponent();

        this.BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as EventsViewModel;
        await vm.GetItemsCommand.ExecuteAsync(null);
    }
}