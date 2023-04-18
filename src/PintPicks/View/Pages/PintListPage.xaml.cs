
using PintPicks.Api.Contract;
using PintPicks.ViewModel;

namespace PintPicks.View.Pages;

public partial class PintListPage : ContentPage
{
    public PintListPage(PintListPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}