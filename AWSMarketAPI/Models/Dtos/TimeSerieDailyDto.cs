using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSMarketAPI.Models.Dtos
{
    public class TimeSerieDailyDto
    {


        [JsonProperty("Meta Data")]
        public MetaDataDto MetaData { get; set; }


        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, ValuesDto> TimeSeries { get; set; }


    }
}
