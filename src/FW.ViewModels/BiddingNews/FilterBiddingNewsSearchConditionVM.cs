using Newtonsoft.Json;

namespace FW.ViewModels.BiddingNews
{
    public class FilterBiddingNewsSearchConditionVM
    {
        [JsonProperty("CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("FromDate")]
        public string FromDate { get; set; }

        [JsonProperty("ToDate")]
        public string ToDate { get; set; }

        [JsonProperty("Authority")]
        public byte? Authority { get; set; }

        [JsonProperty("StatusActive")]
        public bool StatusActive { get; set; }

        public byte? StatusBidding { get; set; } // tinh trang tin thau
    }
}
