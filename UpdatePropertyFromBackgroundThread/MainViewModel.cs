using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UpdatePropertyFromBackgroundThread;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _message = "Waiting";

    [RelayCommand]
    private async Task SetIsBusyBackground()
    {
        await Task.Run(() => IsBusy = !IsBusy);
    }

    [RelayCommand]
    private async Task SetMessageBackground()
    {
        await Task.Run(() => Message = "Hello, World!");
    }

    [RelayCommand]
    private async Task OpenSubWindowInBackground()
    {
        await Task.Run(() =>
        {
            var subWindow = new SubWindow();
            subWindow.Show();
        });
    }

}