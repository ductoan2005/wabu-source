using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.Common.Pagination;
using FW.Common.Pagination.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class BiddingNewsBookmarkBL : BaseBL, IBiddingNewsBookmarkBL
    {
        #region Field

        internal const string ORDER_BY_DEFAULT = "";
        private readonly IBiddingNewsBookmarkRepository _biddingNewsBookmarkRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConstructionRepository _iContructionRepository;
        #endregion

        public BiddingNewsBookmarkBL(IBiddingNewsBookmarkRepository biddingNewsBookmarkRepository, IUnitOfWork unitOfWork, IConstructionRepository iContructionRepository)
        {
            _biddingNewsBookmarkRepository = biddingNewsBookmarkRepository;
            _unitOfWork = unitOfWork;
            _iContructionRepository = iContructionRepository;
        }

        public List<BiddingNewsBookmarkVM> ReadBiddingNewsBookmarkToPagingByCondition(IPaginationInfo iPaginationInfo, UserProfile userProfile, string condition, string orderByStr = null)
        {
            var paginationInfo = new PaginationInfo
            {
                CurrentPage = iPaginationInfo.CurrentPage,
                ItemsPerPage = iPaginationInfo.ItemsPerPage
            };

            if (string.IsNullOrWhiteSpace(orderByStr))
            {
                orderByStr = ORDER_BY_DEFAULT;
            }

            var biddingNewsBookmark = _biddingNewsBookmarkRepository.ReadBiddingNewsBookmarkToPagingByCondition(paginationInfo, userProfile, condition, orderByStr);
            var result = Mapper.Map<List<BiddingNewsBookmark>, List<BiddingNewsBookmarkVM>>(biddingNewsBookmark);
            var currentDateTime = DateTime.UtcNow.AddHours(7);
            //Change status biddingNews
            result.ForEach(x =>
            {
                x.BiddingNews.StatusBiddingNews = 4;
            });

            iPaginationInfo.TotalItems = paginationInfo.TotalItems;
            return result;
        }

        public List<SelectListItem> GetSelectListConstruction()
        {
            return _iContructionRepository.GetMany(a => a.IsDeleted != true)
                .Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.ConstructionName.ToUpper()
                    }).Distinct().ToList();
        }

        #region Private Func


        #endregion
    }
}
