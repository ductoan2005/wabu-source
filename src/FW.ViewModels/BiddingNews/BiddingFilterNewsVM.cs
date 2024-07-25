using System;
using Newtonsoft.Json;

namespace FW.ViewModels.BiddingFilterNews
{
    public class BiddingFilterNewsVM
    {
        [JsonProperty("FromDate")]
        public string FromDate { get; set; }

        [JsonProperty("ToDate")]
        public string ToDate { get; set; }

        [JsonProperty("DateInserted")]
        public string DateInserted { get; set; }

        [JsonProperty("StatusActive")]
        public string StatusActive { get; set; }
    }
}
