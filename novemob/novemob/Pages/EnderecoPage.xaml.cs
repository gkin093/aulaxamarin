using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

using Xamarin.Forms;

namespace novemob
{
	public partial class EnderecoPage : ContentPage
	{
		public EnderecoPage()
		{
			InitializeComponent();
		}

		async void BuscaCEP_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			string sUrl = "https://viacep.com.br/ws/{0}/json/";

			HttpClient client = new HttpClient();

			var uri = new Uri(string.Format(sUrl, txtCep.Text));

			var response = await client.GetAsync(uri);

			CepResult cep = new CepResult();

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				cep = JsonConvert.DeserializeObject<CepResult>(content);

				txtCidade.Text = cep.localidade;
				txtBairro.Text = cep.bairro;
				txtComplemento.Text = cep.complemento;
				txtEstado.Text = cep.uf;
				txtLogradouro.Text = cep.logradouro;

				txtNumero.Focus();

			}
		}
	}
}

