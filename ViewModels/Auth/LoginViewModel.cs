using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Newtonsoft.Json;
using WhatTheCat.Pages;
using WhatTheCat.Pages.Auth;

namespace WhatTheCat.ViewModels.Auth;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string username;
    [ObservableProperty]
    private string password;

    private readonly INavigation _navigation;
    private readonly FirebaseAuthProvider _authProvider;

    public LoginViewModel(INavigation navigation)
    {
        _navigation = navigation;
        _authProvider = new FirebaseAuthProvider(new FirebaseConfig(Models.Constants.WebApiKey));
    }

    [RelayCommand]
    private async Task RegisterBtn()
    {
        await _navigation.PushAsync(new RegisterPage());
    }

    [RelayCommand]
    private async Task LoginBtn()
    {
        try
        {
            var auth = await _authProvider.SignInWithEmailAndPasswordAsync(Username, Password);
            var content = await auth.GetFreshAuthAsync();
            var serializedContent = JsonConvert.SerializeObject(content);
            Preferences.Set("FreshFirebaseToken", serializedContent);
            await _navigation.PushAsync(new Dashboard());
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make(ex.Message, duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }

    [RelayCommand]
    private async Task SignInAnon()
    {
        try
        {
            var auth = await _authProvider.SignInAnonymouslyAsync();
            var content = await auth.GetFreshAuthAsync();
            content.User.Email = "Anonymous";
            var serializedContent = JsonConvert.SerializeObject(content);
            Preferences.Set("FreshFirebaseToken", serializedContent);
            var toast = Toast.Make("Signed in anonymously");
            await toast.Show();
            await _navigation.PushAsync(new Dashboard());
        }
        catch (Exception ex)
        {
            var snackbar = Snackbar.Make(ex.Message, duration: TimeSpan.FromSeconds(5));
            await snackbar.Show();
        }
    }
}
