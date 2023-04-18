namespace PintPicks.View.Pages;
using PintPicks.ViewModel;

public partial class DetailsPage : ContentPage
{

    public DetailsPage(PintViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
