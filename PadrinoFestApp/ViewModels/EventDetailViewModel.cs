using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using PadrinoFestApp.Models;
using PadrinoFestApp.Services;

namespace PadrinoFestApp.ViewModels
{
    [QueryProperty("Item", "Item")]
    public partial class EventDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Evento item;

        IRemoteDatabaseApiService remoteDatabaseApi;

        public EventDetailViewModel(IRemoteDatabaseApiService remoteDatabaseApi)
        {
            this.remoteDatabaseApi = remoteDatabaseApi;
        }

        [RelayCommand]
        private async Task SaveItem()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var isNew = Item.EventoId == 0;

                var query = Item.EventoId != 0
                    ? $"UPDATE eventos SET NombreDelEvento = '{Item.NombreDelEvento}', FechaDelEvento = '{Item.FechaDelEvento}' WHERE EventoId = {Item.EventoId};"
                    : $"INSERT INTO eventos (NombreDelEvento, FechaDelEvento) VALUES('{Item.NombreDelEvento}', '{Item.FechaDelEvento}');";

                var op = await App.LocalDb.ExecuteQuery(query);

                if (op)
                    await Shell.Current.DisplayAlert("Success!", "Data successfully saved!", "OK");
                else
                    await Shell.Current.DisplayAlert("Error!", "Review the data", "OK");

                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
