using FW.Common.Pagination.Interfaces;
using FW.Models;
using FW.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FW.BusinessLogic.Interfaces
{
    public interface IBiddingNewsBookmarkBL
    {
        List<BiddingNewsBookmarkVM> ReadBiddingNewsBookmarkToPagingByCondition(IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null);

        List<SelectListItem> GetSelectListConstruction();
    }
}
