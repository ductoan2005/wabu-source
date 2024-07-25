using FW.BusinessLogic.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.ViewModels;
using AutoMapper;
using FW.Models;
using FW.ViewModels.Home;

namespace FW.BusinessLogic.Implementations
{
    public class HomeBL : BaseBL, IHomeBL
    {
        private readonly IHomeRepository homeRepository;
        private readonly IBiddingPackageRepository biddingPackageRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IUnitOfWork unitOfWork;

        //internal const string ORDER_BY_DEFAULT = "UserCode";

        public HomeBL(IHomeRepository _homeRepository,
            IBiddingPackageRepository _biddingPackageRepository,
            ICompanyRepository _companyRepository,
            IUnitOfWork unitOfWork)
        {
            homeRepository = _homeRepository;
            biddingPackageRepository = _biddingPackageRepository;
            companyRepository = _companyRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsBest()
        {
            var biddingNewsBest = homeRepository.ReadBiddingNewsBest();
            return biddingNewsBest;
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsNewest()
        {
            var biddingNewsNewest = homeRepository.ReadBiddingNewsNewest();
            return biddingNewsNewest;
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsInterest()
        {
            var biddingNewsInterest = homeRepository.ReadBiddingNewsInterest();
            return biddingNewsInterest;
        }

        public IEnumerable<BiddingNewsCommonVM> ReadBiddingNewsSuggest()
        {
            var biddingNewsSuggest = homeRepository.ReadBiddingNewsSuggest();
            return biddingNewsSuggest;
        }

        //Get Công ty(Nhà thầu) uy tín
        public IEnumerable<CompanyRatingVM> GetCompanyRating()
        {
            var companyReputation = companyRepository.GetCompanyRating();

            return companyReputation;
        }

        public IEnumerable<CompanyProfileLogoVM> ReadCompayLogoOnOver()
        {
            var logoOnOver = companyRepository.ReadCompayLogoOnOver();
            return logoOnOver;
        }

        public IEnumerable<BiddingPackageVM> ReadBiddingPackage()
        {
            var biddingPackage = biddingPackageRepository.GetAll();
            var biddingPackageVM = Mapper.Map<IEnumerable<BiddingPackage>, IEnumerable<BiddingPackageVM>>(biddingPackage);
            return biddingPackageVM;
        }
    }
}
