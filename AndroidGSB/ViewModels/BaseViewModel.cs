using CommunityToolkit.Mvvm.ComponentModel;

namespace AndroidGSB.ViewModels;

public abstract class BaseViewModel : ObservableObject
{
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public bool IsNotBusy => !IsBusy;
}

