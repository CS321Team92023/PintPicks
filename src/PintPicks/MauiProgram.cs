using Microsoft.Extensions.Logging;

using PintPicks.Api.Client;
using PintPicks.View.Pages;
using PintPicks.Services;
using PintPicks.ViewModel;

namespace PintPicks;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterPages()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddPintPicksClient(new Uri("https://dl4f2wy9w7.execute-api.us-east-1.amazonaws.com"));
        mauiAppBuilder.Services.AddTransient<PromptService>();
        mauiAppBuilder.Services.AddTransient<PermissionService>();
        mauiAppBuilder.Services.AddTransient<MediaPickerService>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<PintListPageViewModel>();
        mauiAppBuilder.Services.AddTransient<DetailsPageViewModel>();
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
