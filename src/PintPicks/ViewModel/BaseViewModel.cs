using CommunityToolkit.Mvvm.ComponentModel;

namespace PintPicks.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotBusy))]
        private bool isBusy;
        public bool NotBusy => !IsBusy;

        [ObservableProperty]
        private string title;
    }
}
