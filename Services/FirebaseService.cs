using CommunityToolkit.Maui.Alerts;
using Firebase.Auth;
using Newtonsoft.Json;

namespace WhatTheCat.Services;

public class FirebaseService
{
    private readonly FirebaseAuthProvider _authProvider;

    public FirebaseService()
    {
        _authProvider = new FirebaseAuthProvider(new FirebaseConfig(Models.Constants.WebApiKey));
    }

    public async Task LoginAsync(string username, string password)
    {
        var auth = await _authProvider.SignInWithEmailAndPasswordAsync(username, password);
        var content = await auth.GetFreshAuthAsync();
        var serializedContent = JsonConvert.SerializeObject(content);
        Preferences.Set("FreshFirebaseToken", serializedContent);
    }

    public async Task LoginAnonymouslyAsync()
    {
        var auth = await _authProvider.SignInAnonymouslyAsync();
        var content = await auth.GetFreshAuthAsync();
        content.User.Email = "Anonymous";
        var serializedContent = JsonConvert.SerializeObject(content);
        Preferences.Set("FreshFirebaseToken", serializedContent);
        var toast = Toast.Make("Signed in anonymously");
        await toast.Show();
    }

    public async Task RegisterAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            var entryToast = Toast.Make("Please enter a valid email and password");
            await entryToast.Show();
            return;
        }

        if (password.Length < 6)
        {
            var entryToast = Toast.Make("Password must be 6 characters long");
            await entryToast.Show();
            return;
        }

        var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password, sendVerificationEmail: true);
        var token = auth.FirebaseToken;

        var toastText = "";
        if (!string.IsNullOrWhiteSpace(token))
            toastText = "Registration Successful";
        else
            toastText = "Registration Unsuccessful";

        var toast = Toast.Make(toastText);
        await toast.Show();
    }

    public async Task ResetPasswordAsync(string email)
    {
        await _authProvider.SendPasswordResetEmailAsync(email);

        var toast = Toast.Make("Password reset link emailed");
        await toast.Show();
    }
}
