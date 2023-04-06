using CommunityToolkit.Mvvm.ComponentModel;

using PintModel = PintPicks.Api.Contract.Pint;

namespace PintPicks.ViewModel
{
    [QueryProperty(nameof(Pint), "Pint")]
    public partial class DetailsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PintName))]
        PintModel pint;


        public string PintName
        {
            get => Pint?.Name;
        }
    }
}
