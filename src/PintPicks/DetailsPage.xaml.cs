namespace PintPicks;

[QueryProperty(nameof(PintName), "pintName")]
public partial class DetailsPage : ContentPage
{
    private string pintName;
    public string PintName
    {
        get => pintName;
        set
        {
            pintName = Uri.UnescapeDataString(value ?? string.Empty);
            OnPropertyChanged(nameof(PintName));
        }
    }

    public DetailsPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
