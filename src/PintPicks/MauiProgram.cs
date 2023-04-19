using Microsoft.Extensions.Logging;

using PintPicks.Api.Client;
using PintPicks.View.Pages;
using PintPicks.Services;
using PintPicks.ViewModel;
using CommunityToolkit.Maui;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace PintPicks;

public static class MauiProgram
{


    public static MauiApp CreateMauiApp()
	{
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("PintPicks.appsettings.json");

        IConfiguration config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .RegisterAppServices(config)
            .RegisterViewModels()
            .RegisterPages()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Configuration.AddConfiguration(config);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
	}

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder, IConfiguration config)
    {
        var settings = config.GetRequiredSection("Settings").Get<Settings>();
        mauiAppBuilder.Services.AddPintPicksClient(new Uri(settings.ApiUrl));
        mauiAppBuilder.Services.AddTransient<PromptService>();
        mauiAppBuilder.Services.AddTransient<PermissionService>();
        mauiAppBuilder.Services.AddTransient<MediaPickerService>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<PintListPageViewModel>();
        mauiAppBuilder.Services.AddTransient<PintViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPage>();
        mauiAppBuilder.Services.AddTransient<PintListPage>();
        mauiAppBuilder.Services.AddTransient<DetailsPage>();
        return mauiAppBuilder;
    }
}
