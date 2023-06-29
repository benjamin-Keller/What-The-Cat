using WhatTheCat.Services;
using WhatTheCat.ViewModels.Auth;

namespace WhatTheCat.Pages.Auth;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterViewModel(Navigation, new FirebaseService());
	}
}