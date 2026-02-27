using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace phoenix_prototype
{
    public class SearchEntry
    {

        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("nationality")]
        public string Nationality { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("rating")]
        public int Rating  { get; set; }
        [JsonPropertyName("telephone")]
        public string Telephone { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("r100")]
        public int R100 { get; set; }
        [JsonPropertyName("r150")]
        public int R150 { get; set; }
        [JsonPropertyName("r200")]
        public int R200 { get; set; }

        [JsonPropertyName("minimumCharge")]
        public int MinimumCharge { get; set; }


        [JsonPropertyName("maximumCharge")]
        public int MaximumCharge { get; set; }
        
        [JsonPropertyName("numberOfDaysInService")]
        public int NumberOfDaysInService { get; set; }
        
        [JsonPropertyName("percentageAvailable")]
        public int PercentageAvailable { get; set; }
        
        [JsonPropertyName("totalRegionsTravelled")]
        public int TotalRegionsTravelled { get; set; }
        
        [JsonPropertyName("previouslyServicedBB")]
        public bool PreviouslyServicedBB { get; set; }
    }
}
