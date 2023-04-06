using PintPicks.ViewModel;

namespace PintPicks.View.Pages;
public partial class MainPage : ContentPage 
{
    public MainPage(MainPageViewModel viewModel) 
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}