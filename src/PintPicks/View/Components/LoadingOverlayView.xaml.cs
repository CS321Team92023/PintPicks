using System.Collections.ObjectModel;

namespace PintPicks.View.Components;

public partial class LoadingOverlayView : ContentView
{
	public LoadingOverlayView()
	{
		InitializeComponent();
	}

    public string LoadingText
    {
        get
        {
            return (string)GetValue(LoadingTextProperty);
        }
        set
        {
            SetValue(LoadingTextProperty, value);
        }
    }
    public static readonly BindableProperty LoadingTextProperty =
        BindableProperty.Create(nameof(LoadingText), typeof(string), typeof(LoadingOverlayView), null);

    public bool IsRunning
    {
        get
        {
            return (bool)GetValue(IsRunningProperty);
        }
        set
        {
            SetValue(IsRunningProperty, value);
        }
    }
    public static readonly BindableProperty IsRunningProperty =
        BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(LoadingOverlayView), true);

}