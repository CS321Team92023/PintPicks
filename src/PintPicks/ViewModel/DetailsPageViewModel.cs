using CommunityToolkit.Mvvm.ComponentModel;
using PintPicks.Api.Contract;
using PintModel = PintPicks.Api.Contract.Pint;

namespace PintPicks.ViewModel
{
    [QueryProperty(nameof(Pint), "Pint")]
    public partial class DetailsPageViewModel : BaseViewModel
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
        public IEnumerable<PintRating> Ratings
        {
            get => Pint?.Ratings;
        }


        public DetailsPageViewModel(){}
        public DetailsPageViewModel(PintModel pint) {
            this.pint = pint;
        }

       
        public float OverallRating {
            get {
                if (Pint == null || Pint.Ratings == null)
                {
                    return 0;
                }
                IEnumerable<PintRating> Ratings = Pint?.Ratings;
                float count = 0;
                int num = 0;
                foreach (PintRating rating in Ratings)
                {
                    count += rating.ReviewOverall;
                    num += 1;
                }
                float averageRating = (count / num) + 0.5f;
                return averageRating;
            }
        }


    }
}