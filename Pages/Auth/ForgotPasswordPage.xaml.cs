using WhatTheCat.Services;
using WhatTheCat.ViewModels.Auth;

namespace WhatTheCat.Pages.Auth;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage()
	{
		InitializeComponent();
		BindingContext = new ForgotPasswordViewModel(Navigation, new FirebaseService());
	}
}