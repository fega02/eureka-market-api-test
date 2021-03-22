using AWSMarketAPI.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AWSMarketAPI.Utils.HttpClient
{
    public class ClientHttpService
    {
      
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public ClientHttpService(System.Net.Http.HttpClient client, IOptions<AppSettings> appSettings)
        {
            _httpClient = client;
            _appSettings = appSettings.Value;
        }


        public async Task<string> GetPricesJSONAsync(string stockSymbol)
        {
            HttpResponseMessage response;
            try
            {
        
                response = await _httpClient.GetAsync($"/query?function=TIME_SERIES_DAILY&symbol={stockSymbol}&outputsize=compact&apikey={_appSettings.APIKeyAlphaVantage}");

                if ((int)response.StatusCode == 200)
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception("Error trying to retrive data from API AlphaVantage");
                }
               

            }
            catch (Exception e)
            {
                throw e;
            }
        }




    }
}
