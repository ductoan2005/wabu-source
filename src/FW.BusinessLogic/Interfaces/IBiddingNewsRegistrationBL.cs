using FW.ViewModels;
using FW.ViewModels.BiddingNewsRegistration;
using System.Collections.Generic;
using FW.Models;
using FW.ViewModels.BiddingNews;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.BusinessLogic.Interfaces
{
    public interface IBiddingNewsRegistrationBL
    {


        IEnumerable<BiddingPackageVM> ReadAllBiddingPackage();

        IEnumerable<WorkContentVM> ReadAllWorkContentByBiddingPackage(long? Id);

        IEnumerable<ContractFormVM> ReadAllContractForm();

        Task<JsonResult> CreateBiddingNewsReturnBiddingNewsId(BiddingNewsRegistrationVM biddingNewsRegistrationVM, UserProfile userProfile);
    }
}
