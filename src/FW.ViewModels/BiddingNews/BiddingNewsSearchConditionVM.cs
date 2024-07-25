using Newtonsoft.Json;

namespace FW.ViewModels.BiddingNews
{
    public class BiddingNewsSearchConditionVM
    {
        [JsonProperty("FromDate")]
        public string FromDate { get; set; }

        [JsonProperty("ToDate")]
        public string ToDate { get; set; }

        [JsonProperty("ContructionName")]
        public string ContructionName { get; set; }

        [JsonProperty("BiddingPackageName")]
        public string BiddingPackageName { get; set; }

       [JsonProperty("BiddingPackageType")]
        public byte BiddingPackageType { get; set; }

        //[JsonProperty("Status")]
        //public byte? Status { get; set; }

        [JsonProperty("Authority")]
        public byte? Authority { get; set; }

        [JsonProperty("AreaId")]
        public long? AreaId { get; set; }

        [JsonProperty("NumberYearActivityAbilityExp")]
        public string NumberYearActivityAbilityExp { get; set; }

        [JsonProperty("NumberSimilarContractAbilityExp")]
        public string NumberSimilarContractAbilityExp { get; set; }

        [JsonProperty("Turnover2YearAbilityFinance")]
        public string Turnover2YearAbilityFinance { get; set; }

        [JsonProperty("JobPosition")]
        public string JobPosition { get; set; }

        [JsonProperty("TextSearch")]
        public string TextSearch { get; set; }

        [JsonProperty("BiddingPackageId")]
        public long? BiddingPackageId { get; set; }

        [JsonProperty("StatusBidding")]
        public byte? StatusBidding { get; set; }
    }
}
