using PintPicks.View.Pages;

namespace PintPicks;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(PintListPage), typeof(PintListPage));
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}
