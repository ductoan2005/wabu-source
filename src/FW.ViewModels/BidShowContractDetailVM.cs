namespace FW.ViewModels
{
    public class BidShowContractDetailVM
    {
        public bool CheckBiddingNewsSelected { get; set; }

        public bool CheckBiddingNewsCompleted { get; set; }

        public bool CheckConstructionByCompanyProfile { get; set; }

        public BiddingDetailVM ListFileWithProfile { get; set; } = new BiddingDetailVM();

        public Models.BiddingNews BiddingNews { get; set; }
    }
}
