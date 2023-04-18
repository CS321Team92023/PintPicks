using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PintPicks.Api.Contract;
using PintPicks.View.Pages;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace PintPicks.ViewModel
{
    [QueryProperty(nameof(Pints), "Pints")]
    public partial class PintListPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SortedPints))]
        ObservableCollection<Pint> pints;


        public ObservableCollection<PintViewModel> SortedPints 
        { 
            get
            {
                if (Pints == null)
                {
                    return null;
                }
                IEnumerable<PintViewModel> sortedData = Pints.ToList().Select(d => new PintViewModel(d));

                if(SortOrder == Order.Asc)
                {
                    sortedData = SortBy switch
                    {
                        "Rating" => sortedData.OrderBy(p => p.OverallRating),
                        "Name" => sortedData.OrderBy(p => p.Pint?.Name),
                        "ABV" => sortedData.OrderBy(p => p.Pint?.ABV),
                        "Style" => sortedData.OrderBy(p => p.Pint?.Style?.Name),
                        _ => sortedData
                    };
                }
                else
                {
                    sortedData = SortBy switch
                    {
                        "Rating" => sortedData.OrderByDescending(p => p.OverallRating),
                        "Name" => sortedData.OrderByDescending(p => p.Pint?.Name),
                        "ABV" => sortedData.OrderByDescending(p => p.Pint?.ABV),
                        "Style" => sortedData.OrderByDescending(p => p.Pint?.Style?.Name),
                        _ => sortedData
                    };
                }

                return new ObservableCollection<PintViewModel>(sortedData);
            }
        }

        public enum Order
        {
            Asc,
            Desc
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SortedPints))]
        string sortBy;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SortedPints))]
        [NotifyPropertyChangedFor(nameof(SortOrderDisplay))]
        Order sortOrder;

        public string SortOrderDisplay => SortOrder.ToString();

        public PintListPageViewModel() 
        {
            Title = "Results";
            SortBy = "Rating";
            SortOrder = Order.Desc;
        }

        [RelayCommand]
        private async Task AscDescButtonTapped()
        {
            SortOrder = SortOrder == Order.Asc ? Order.Desc : Order.Asc;
        }

        [RelayCommand]
        private async Task PintTapped(PintViewModel pint)
        {
            if (pint == null)
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


        private async Task NavigateToPintDetailsPage(PintViewModel pint)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Pint", pint.Pint }
            };
            await Shell.Current.GoToAsync(nameof(DetailsPage), navigationParameter);
        }
    }
}
