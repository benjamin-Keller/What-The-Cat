using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Newtonsoft.Json;
using WhatTheCat.Pages;
using WhatTheCat.Pages.Auth;
using WhatTheCat.Services;

namespace WhatTheCat.ViewModels.Auth;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string username;
    [ObservableProperty]
    private string password;

    private readonly INavigation _navigation;
    private readonly FirebaseService _firebaseService;

    public LoginViewModel(INavigation navigation, FirebaseService firebaseService)
    {
        _navigation = navigation;
        _firebaseService = firebaseService;
    }

    [RelayCommand]
    private async Task LoginBtn()
    {
        try
        {
            await _firebaseService.LoginAsync(Username, Password);
            await _navigation.PushAsync(new Dashboard());
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make("Something went wrong", duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }

    [RelayCommand]
    private async Task SignInAnon()
    {
        try
        {
            await _firebaseService.LoginAnonymouslyAsync();
            await _navigation.PushAsync(new Dashboard());
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make("Something went wrong", duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }

    [RelayCommand]
    private async Task RegisterBtn()
    {
        await _navigation.PushAsync(new RegisterPage());
    }

    [RelayCommand]
    private async Task ForgotPasswordBtn()
    {
        await _navigation.PushAsync(new ForgotPasswordPage());
    }
}
