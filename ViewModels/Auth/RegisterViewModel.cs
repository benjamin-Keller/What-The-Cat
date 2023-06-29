using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WhatTheCat.Services;

namespace WhatTheCat.ViewModels.Auth;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string password;

    private readonly INavigation _navigation;
    private readonly FirebaseService _firebaseService;

    public RegisterViewModel(INavigation navigation, FirebaseService firebaseService)
    {
        _navigation = navigation;
        _firebaseService = firebaseService;
    }

    [RelayCommand]
    private async Task RegisterUser()
    {
        try
        {
            await _firebaseService.RegisterAsync(Email, Password);
            await _navigation.PopAsync();
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make("Something went wrong", duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }
}
