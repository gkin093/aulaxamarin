using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace novemob
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			BindingContext = new MainPageViewModel();
			InitializeComponent();

		}
	}
}
