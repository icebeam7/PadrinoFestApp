using PadrinoFestApp.Helpers;
using PadrinoFestApp.Services;
using PadrinoFestApp.ViewModels;
using PadrinoFestApp.Views;

namespace PadrinoFestApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        var dbPath = FileAccessHelper.GetLocalFilePath(Constants.PadrinoFestLocalDb);

        builder.Services.AddSingleton<ILocalDatabaseService>(
            s => ActivatorUtilities
				.CreateInstance<LocalDatabaseService>(s, dbPath));

        builder.Services.AddSingleton<IRemoteDatabaseApiService, RemoteDatabaseApiService>();

        builder.Services.AddTransient<EventsViewModel>();
        builder.Services.AddTransient<EventsView>();

        builder.Services.AddTransient<EventDetailViewModel>();
        builder.Services.AddTransient<EventDetailView>();

        builder.Services.AddTransient<SpeakersViewModel>();
        builder.Services.AddTransient<SpeakersView>();

        builder.Services.AddTransient<SpeakerDetailViewModel>();
        builder.Services.AddTransient<SpeakerDetailView>();

        return builder.Build();
	}
}
