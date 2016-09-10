using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace novemob
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			UserDialogs.Instance.ShowLoading("Logando como: " + txtLogin.Text);
			await Task.Delay(2000);

			await Navigation.PushAsync(new CadastroUsuarioPage());

			UserDialogs.Instance.HideLoading();
		}

		void Rec_Clicked(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}

