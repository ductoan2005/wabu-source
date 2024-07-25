using FW.ViewModels;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.ViewModels.Home;

namespace FW.BusinessLogic.Interfaces
{
    public interface IHomeBL
    {
        IEnumerable<BiddingPackageVM> ReadBiddingPackage(); // get data control select

        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsBest(); // section Gói Thầu Tốt Nhất

        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsNewest(); // tab Gói Thầu Mới Nhất

        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsInterest(); // tab Gói Thầu Được Quan Tâm Nhất

        IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsSuggest(); // tab Gói Thầu Được Đề Xuất

        IEnumerable<CompanyRatingVM> GetCompanyRating(); // tab Gói Thầu Được Đề Xuất
        IEnumerable<CompanyProfileLogoVM> ReadCompayLogoOnOver(); // logo nhà thầu nổi bật
    }
}
