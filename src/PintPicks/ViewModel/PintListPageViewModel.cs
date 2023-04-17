using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PintPicks.Api.Contract;
using PintPicks.View.Pages;
using System.Collections.ObjectModel;
using System.Linq;

namespace PintPicks.ViewModel
{
    [QueryProperty(nameof(Pints), "Pints")]
    public partial class PintListPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SortedPints))]
        ObservableCollection<Pint> pints;


        public ObservableCollection<DetailsPageViewModel> SortedPints 
        { 
            get
            {
                if (Pints == null)
                {
                    return null;
                }
                IEnumerable<DetailsPageViewModel> sortedData = Pints.ToList().Select(d => new DetailsPageViewModel(d));
                sortedData = SortBy switch
                {
                    "Rating" => sortedData.OrderBy(p => p.OverallRating),
                    "Name" => sortedData.OrderBy(p => p.Pint?.Name),
                    "ABV" => sortedData.OrderBy(p => p.Pint?.ABV),
                    "Style" => sortedData.OrderBy(p => p.Pint?.Style?.Name),
                    _ => sortedData
                };
                return new ObservableCollection<DetailsPageViewModel>(sortedData);
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SortedPints))]
        string sortBy;

        public PintListPageViewModel() 
        {
            Title = "Results";
            SortBy = "Rating";
        }

        [RelayCommand]
        private async Task PintTapped(Pint pint)
        {
            if (pint != null)
            {
                return;
            }
            try
            {
                await NavigateToPintDetailsPage(pint);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "There was an error selecting a pint", "Ok");
            }
        }


        private async Task NavigateToPintDetailsPage(Pint pint)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Pint", pint }
            };
            await Shell.Current.GoToAsync(nameof(DetailsPage), navigationParameter);
        }
    }
}
