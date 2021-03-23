using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AWSMarketAPI.Controllers.Base;
using AWSMarketAPI.Models.Dtos;
using AWSMarketAPI.Utils;
using AWSMarketAPI.Utils.HttpClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AWSMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockPricesController : StockBaseController
    {

        private readonly ClientHttpService _clientHttpService;
        public StockPricesController(ClientHttpService client, ILogger<StockPricesController> logger) : base(logger)
        {
            _clientHttpService = client;
        }


        [HttpGet("{stockSymbol}")]
        public async Task<IActionResult> GetStock(string stockSymbol)
        {

            try
            {
                if (string.IsNullOrEmpty(stockSymbol))
                {
                    return Ok(new ResponseBase() { Code = 202, Message = "The parameter 'stockSymbol' is empty", Success = false });
                }
                var responseStock = await _clientHttpService.GetPricesJSONAsync(stockSymbol);
              

              var dtoCompetition = JsonConvert.DeserializeObject<TimeSerieDailyDto>(responseStock, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });
                var result = GetInformation(dtoCompetition);
                return Ok(result);
            }
            catch (JsonSerializationException ex)
            {
                _logger.LogError("An error has occurred trying to Serialize Json from  AlphaVantage" + ex);
                Console.WriteLine(ex.Message);
                // throw ex;
                return Ok(new ResponseBase() { Code = 202, Message = "The system could not process the results of alpha vantage. Please contact the system administrator ", Success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occurred" + ex);
                 throw ex;
            }


          
        }

        private Dictionary<string, DataPricesDto> GetInformation(TimeSerieDailyDto data)
        {
            var dataInformation = new Dictionary<string, DataPricesDto>();

            string lastDay = data.TimeSeries.FirstOrDefault().Key;
            foreach (var item in data.TimeSeries)
            {

                // lastDay = item.Key;
                if (dataInformation.Any())
                {
                    var lastInformationItem = dataInformation[lastDay];
                    var lastClose = lastInformationItem.Close;
                    var currentClose = item.Value.Close;
                    var diff = lastClose - currentClose;

                    dataInformation[lastDay].Variation = diff;

                    dataInformation.Add(item.Key, new DataPricesDto
                    {
                        Close = item.Value.Close,
                        Low = item.Value.Low,
                        High = item.Value.High,
                        Open = item.Value.Open
                    });
                }
                else
                {
                    dataInformation.Add(item.Key, new DataPricesDto
                    {
                        Close = item.Value.Close,
                        Low = item.Value.Low,
                        High = item.Value.High,
                        Open = item.Value.Open
                    });
                }



                //var lastClose = item.Value.Close;
                lastDay = item.Key;



            }

            return dataInformation;
        }


    }
}
