using CommunityToolkit.Mvvm.ComponentModel;
using Rating = PintPicks.Api.Contract.PintRating;
using PintModel = PintPicks.Api.Contract.Pint;
using System.Linq;

namespace PintPicks.ViewModel
{
    [QueryProperty(nameof(Pint), "Pint")]
    public partial class PintViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PintName))]
        [NotifyPropertyChangedFor(nameof(OverallRating))]
        PintModel pint;

        public string PintName
        {
            get => Pint?.Name;
        }

        //Gets ratings array
        public IEnumerable<Rating> Ratings
        {
            get => Pint?.Ratings;
        }


        public PintViewModel(){}
        public PintViewModel(PintModel pint) {
            this.pint = pint;
        }

       
        public float OverallRating {
            get {
                if (Pint == null || Pint.Ratings == null)
                    return 0;

                float averageRating = Pint.Ratings.Select(rating => rating.ReviewOverall)
                    .DefaultIfEmpty(0)
                    .Average();

                return averageRating + 0.5f;
            }
        }


    }
}