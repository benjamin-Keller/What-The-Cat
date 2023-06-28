using Newtonsoft.Json;

namespace WhatTheCat.Pages;

public partial class Dashboard : ContentPage
{
	public Dashboard()
	{
		InitializeComponent();
		GetProfileInfo();
	}

	private async Task GetProfileInfo()
	{
		var userInfo = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FreshFirebaseToken", ""));
		UserName.Text = userInfo.User.Email;
	}
}