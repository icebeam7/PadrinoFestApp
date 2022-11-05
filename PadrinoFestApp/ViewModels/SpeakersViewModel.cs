using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using PadrinoFestApp.Models;
using PadrinoFestApp.Helpers;
using PadrinoFestApp.Services;

namespace PadrinoFestApp.ViewModels
{
    public partial class SpeakersViewModel : BaseViewModel
    {
        public ObservableCollection<Speaker> Items { get; } = new();

        [ObservableProperty]
        Speaker selectedItem;

        IRemoteDatabaseApiService remoteDatabaseApi;

        public SpeakersViewModel(IRemoteDatabaseApiService remoteDatabaseApi)
        {
            Title = "Lista de Speaker";
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

                var items = await App.LocalDb.GetItemsWithQuery<Speaker>("select * from speakers");

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

        private async Task NavigateToDetails(Speaker item)
        {
            var data = new Dictionary<string, object>
            {
                { "Item", item }
            };

            await Shell.Current.GoToAsync(Constants.SpeakerDetailsRoute, true, data);
        }

        [RelayCommand]
        private async Task GoToNewDetails()
        {
            await NavigateToDetails(new Speaker());
        }

        [RelayCommand]
        private async Task GoToDetails()
        {
            if (selectedItem == null)
                return;

            await NavigateToDetails(selectedItem);
        }

        [RelayCommand]
        private async Task DeleteItem(Speaker item)
        {
            var op = await App.LocalDb.ExecuteQuery($"DELETE FROM speakers WHERE SpeakerId = {item.SpeakerId}");

            if (op)
                await Shell.Current.DisplayAlert("Success!", "Data successfully deleted!", "OK");
            else
                await Shell.Current.DisplayAlert("Error!", "Try again later", "OK");

            await GetItemsAsync();
        }
    }
}
