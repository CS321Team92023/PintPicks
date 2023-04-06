
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

    private async void OnPintClicked(object sender, EventArgs e) 
    {
        try
        {

        }
        catch (Exception)
        {
            await DisplayAlert("Error", "There was an error selecting a pint.", "Ok");
        }
    }

    private async void PintItemTapped(object sender, TappedEventArgs e)
    {
        var pint = ((VisualElement)sender).BindingContext as Pint;
        if (pint != null)
        {
            return;
        }

        //await Shell.Current.GoToAsync(nameof(PintDetailsPage), true, new Dictionary<string, object> {
        //{
        //    "Pint", pint
        //} });
    }
}