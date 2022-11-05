using PadrinoFestApp.Models;
using PadrinoFestApp.Helpers;
using PadrinoFestApp.Services;

using System.Text.Json;
using Microsoft.Maui.Controls;

namespace PadrinoFestApp;

public partial class App : Application
{
    public static ILocalDatabaseService LocalDb { get; private set; }

    public App(ILocalDatabaseService localDb,
        IRemoteDatabaseApiService remoteDatabaseApi)
    {
        InitializeComponent();

        LocalDb = localDb;

        MainPage = new AppShell();
    }
}
