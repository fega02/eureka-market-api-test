using System.Text.Json.Serialization;

namespace AWSMarketAPI.Models.Dtos
{
    internal class DataPricesDto
    {


        public decimal Open { get; set; }

        public decimal High { get; set; }


        public decimal Low { get; set; }

        [JsonIgnore]
        public decimal Close { get; set; }


        public decimal Variation { get; set; }
    }
}