using System;
using Newtonsoft.Json;

namespace novemob
{
	public class CepResult
	{
		[JsonProperty("logradouro")]
		public string logradouro { get; set;}

		[JsonProperty("complemento")]
		public string complemento { get; set; }

		[JsonProperty("bairro")]
		public string bairro { get; set; }

		[JsonProperty("localidade")]
		public string localidade { get; set; }

		[JsonProperty("uf")]
		public string uf { get; set; }

		public CepResult()
		{
		}
	}
}

