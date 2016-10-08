using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Xamarin.Forms;

//Utilizados para geolocalização
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace novemob
{
	public partial class EnderecoPage : ContentPage
	{
		Geocoder geoCoder;

		public EnderecoPage()
		{
			InitializeComponent();

			geoCoder = new Geocoder();
			//geoloc();
		}

		async void geoloc()
		{
			//pegar dados atuais de geolocation
			var locator = CrossGeolocator.Current;
			//definindo a exatidão da geolocalização
			locator.DesiredAccuracy = 100;

			//pegando a posição do usuário
			var position = await locator.GetPositionAsync(1000);

			string latitude = position.Latitude.ToString();
			string longitude = position.Longitude.ToString();

			//pegando o endereço das coordenadas
			var enderecoPossivel = await geoCoder.GetAddressesForPositionAsync(new Position(position.Latitude, position.Longitude));
			//pegar endereco possivel e exibir na label
			foreach (var endereco in enderecoPossivel)
			{
				lblEndereco.Text += endereco + "\n";
			}

			//marcar o pin no mapa
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));
			var pin = new Pin
			{
				Type = PinType.Place,
				Position = new Position(position.Latitude, position.Longitude),
				Label = "Minha Localização",
				Address = lblEndereco.Text
			};
			map.Pins.Add(pin);

			//escreve latitude e longitude na tela
			lblLat.Text = latitude;
			lblLog.Text = longitude;
		}

		async void BuscaCoordenada_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			//pegar dados atuais de geolocation
			var locator = CrossGeolocator.Current;
			//definindo a exatidão da geolocalização
			locator.DesiredAccuracy = 100;

			//pegando as coordenadas do endereço
			var localizacaoPossivel = await geoCoder.GetPositionsForAddressAsync(txtEndereco.Text);

			foreach (var posicao in localizacaoPossivel)
			{
				//escreve latitude e longitude na tela
				lblLat.Text = posicao.Latitude.ToString();
				lblLog.Text = posicao.Longitude.ToString();
			}

			//URL de serviço free sobre clima
			string url = "http://api.geonames.org/findNearByWeatherJSON?lat={0}&lng={1}&username=deznetfiap";

			//criando requisição rest
			HttpClient cliente = new HttpClient();

			//string com os parametros para a URL
			var uri = new Uri(string.Format(url, new object[] { lblLat.Text, lblLog.Text}));

			//fazendo um GET no serviço
			var response = await cliente.GetAsync(uri);

			//classe para deserializar o retorno
			TempoResultModel tempo = new TempoResultModel();
			//verificando se o status code é OK
			if (response.IsSuccessStatusCode)
			{
				//pegando o retorno e lendo em async
				var content = await response.Content.ReadAsStringAsync();
				//deserializando o conteúdo para a classe result
				tempo = JsonConvert.DeserializeObject<TempoResultModel>(content);

				lblTemp.Text = tempo.weatherObservation.temperature;
				lblLocalizacaoEstacao.Text = tempo.weatherObservation.stationName;

			}

			//marcar o pin no mapa
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(double.Parse(lblLat.Text), double.Parse(lblLog.Text)), Distance.FromMiles(1)));
			var pin = new Pin
			{
				Type = PinType.Place,
				Position = new Position(double.Parse(lblLat.Text), double.Parse(lblLog.Text)),
				Label = "Minha Localização",
				Address = txtEndereco.Text
			};
			map.Pins.Add(pin);


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

