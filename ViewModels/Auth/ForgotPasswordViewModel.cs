using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WhatTheCat.Services;

namespace WhatTheCat.ViewModels.Auth;

public partial class ForgotPasswordViewModel : ObservableObject
{
    [ObservableProperty]
    private string email;

    private readonly INavigation _navigation;
    private readonly FirebaseService _firebaseService;

    public ForgotPasswordViewModel(INavigation navigation, FirebaseService firebaseService)
    {
        _navigation = navigation;
        _firebaseService = firebaseService;
    }

    [RelayCommand]
    private async Task ForgotPassword()
    {
        try
        {
            await _firebaseService.ResetPasswordAsync(Email);
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make("Something went wrong", duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }
}
