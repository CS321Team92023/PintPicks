using CommunityToolkit.Mvvm.ComponentModel;
using Rating = PintPicks.Api.Contract.PintRating;
using PintModel = PintPicks.Api.Contract.Pint;

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
                {
                    return 0;
                }
                IEnumerable<Rating> Ratings = Pint?.Ratings;
                float count = 0;
                int num = 0;
                foreach (Rating rating in Ratings)
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