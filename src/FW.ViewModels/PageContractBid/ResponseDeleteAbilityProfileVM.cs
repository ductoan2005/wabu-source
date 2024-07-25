using FW.Common.Enum;

namespace FW.ViewModels.PageContractBid
{
    public class ResponseDeleteAbilityProfileVM
    {
        public long? Id { get; set; }
        public StatusDelete StatusDelete { get; set; }
        public string AbilityName { get; set; }
    }
}
