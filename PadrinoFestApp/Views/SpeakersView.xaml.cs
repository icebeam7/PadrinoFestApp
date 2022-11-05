using PadrinoFestApp.ViewModels;

namespace PadrinoFestApp.Views;

public partial class SpeakersView : ContentPage
{
    public SpeakersView(SpeakersViewModel vm)
    {
        InitializeComponent();

        this.BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as SpeakersViewModel;
        await vm.GetItemsCommand.ExecuteAsync(null);
    }
}