using WhatTheCat.Services;
using WhatTheCat.ViewModels.Auth;

namespace WhatTheCat;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel(Navigation, new FirebaseService());
    }
}