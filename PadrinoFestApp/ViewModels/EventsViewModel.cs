using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using PadrinoFestApp.Models;
using PadrinoFestApp.Helpers;
using PadrinoFestApp.Services;

namespace PadrinoFestApp.ViewModels
{
    public partial class EventsViewModel : BaseViewModel
    {
        public ObservableCollection<Evento> Items { get; } = new();

        [ObservableProperty]
        Evento selectedItem;

        IRemoteDatabaseApiService remoteDatabaseApi;

        public EventsViewModel(IRemoteDatabaseApiService remoteDatabaseApi)
        {
            Title = "Lista de Eventos";
            this.remoteDatabaseApi = remoteDatabaseApi;
        }

        [RelayCommand]
        private async Task GetItemsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var items = await App.LocalDb.GetItemsWithQuery<Evento>("select * from eventos");

                if (Items.Count != 0)
                    Items.Clear();

                foreach (var item in items)
                    Items.Add(item);
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

        private async Task NavigateToDetails(Evento item)
        {
            var data = new Dictionary<string, object>
            {
                { "Item", item }
            };

            await Shell.Current.GoToAsync(Constants.EventDetailsRoute, true, data);
        }

        [RelayCommand]
        private async Task GoToNewDetails()
        {
            await NavigateToDetails(new Evento());
        }

        [RelayCommand]
        private async Task GoToDetails()
        {
            if (selectedItem == null)
                return;

            await NavigateToDetails(selectedItem);
        }

        [RelayCommand]
        private async Task DeleteItem(Evento item)
        {
            var op = await App.LocalDb.ExecuteQuery($"DELETE FROM eventos WHERE EventoId = {item.EventoId}");

            if (op)
                await Shell.Current.DisplayAlert("Success!", "Data successfully deleted!", "OK");
            else
                await Shell.Current.DisplayAlert("Error!", "Try again later", "OK");

            await GetItemsAsync();
        }
    }
}
