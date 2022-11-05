using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using PadrinoFestApp.Models;
using PadrinoFestApp.Services;

namespace PadrinoFestApp.ViewModels
{
    [QueryProperty("Item", "Item")]
    public partial class SpeakerDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Speaker item;

        IRemoteDatabaseApiService remoteDatabaseApi;

        public SpeakerDetailViewModel(IRemoteDatabaseApiService remoteDatabaseApi)
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
                var isNew = Item.SpeakerId == 0;

                var query = Item.SpeakerId != 0
                    ? $"UPDATE speakers SET NombreDelSpeaker = '{Item.NombreDelSpeaker}', EventoId = '{Item.EventoId}' WHERE SpeakerId = {Item.SpeakerId};"
                    : $"INSERT INTO speakers (NombreDelSpeaker, EventoId) VALUES('{Item.NombreDelSpeaker}', '{Item.EventoId}');";

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
