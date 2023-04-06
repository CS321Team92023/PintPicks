namespace PintPicks.View.Pages;
using PintPicks.ViewModel;

public partial class DetailsPage : ContentPage
{

    public DetailsPage(DetailsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
