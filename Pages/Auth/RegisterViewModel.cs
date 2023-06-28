using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace WhatTheCat.Pages.Auth;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string email;
    [ObservableProperty]
    private string password;

    private readonly INavigation _navigation;

    public RegisterViewModel(INavigation navigation)
    {
        _navigation = navigation;
    }

    [RelayCommand]
    private async Task RegisterUser()
    {
        try
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Models.Constants.WebApiKey));
            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password, sendVerificationEmail: true);
            var token = auth.FirebaseToken;

            var toastText = "";
            if (!string.IsNullOrWhiteSpace(token))
                toastText = "Registration Successful";
            else
                toastText = "Registration Unsuccessful";

            var toast = Toast.Make(toastText);
            await toast.Show();
            await _navigation.PopAsync();
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make(ex.Message, duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }
}
