using System.Threading.Tasks;
using System.Web.Mvc;
using FW.ViewModels;
using FW.ViewModels.BiddingNews;

namespace FW.BusinessLogic.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IBiddingDetailBL
    {
        /// <summary>
        /// ChooseContractorBidding
        /// </summary>
        /// <param name="biddingNewsId"></param>
        /// <param name="companyProfileId"></param>
        /// <returns></returns>
        Task<JsonResult> ChooseContractorBidding(long? biddingNewsId, long? companyProfileId);
        Task<JsonResult> ConfirmContractorBidding(long? biddingNewsId, long? companyProfileId);
        Task<JsonResult> CancelContractorBidding(long? biddingNewsId, long? companyProfileId);
        Task<JsonResult> CompletedContractorBidding(long? biddingNewsId);
        
        /// <summary>
        /// GetBiddingDetailToEditById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BiddingDetailVM> GetBiddingDetailToEditById(long? id, long? biddingNewsId, long? userId);

        /// <summary>
        /// Create bidding detail and detail files
        /// </summary>
        /// <param name="printInfoBiddingVm"></param>
        Task CreateBiddingDetail(PrintInfoBiddingVM printInfoBiddingVm);

        /// <summary>
        /// UpdateBiddingInfoDetail
        /// </summary>
        /// <param name="printInfoBiddingVm"></param>
        Task UpdateBiddingInfoDetail(PrintInfoBiddingVM printInfoBiddingVm);

        /// <summary>
        /// DeleteBiddingInfoDetail
        /// </summary>
        /// <param name="id"></param>
        Task DeleteBiddingInfoDetail(long? id);

        Task<BiddingDetailVM> GetBiddingDetailByProfileId(long? biddingNewsId, long? companyProfileId);

        Task<BiddingDetailVM> GetBiddingNewsById(long? biddingNewsId);

        string GetAllCompanyAbilityExpByListId(string listId);

        string GetAllCompanyAbilityFinanceByListId(string listId);

        string GetAllCompanyAbilityHrsByListId(string listId);

        string GetAllCompanyAbilityEquipmentByListId(string listId);
    }
}
