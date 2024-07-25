using FW.Common.Enum;
using FW.Common.Helpers;
using FW.Common.Pagination.Interfaces;
using System;

namespace FW.Common.Pagination
{
    public class PaginationInfo : IPaginationInfo
    {
        #region Properties
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PagesPerBlock { get; set; }
        public int TotalPages
        {
            get
            {
                if (ItemsPerPage == 0)
                {
                    return 0;
                }
                else
                {
                    return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
                }
            }
        }

        public bool ItemsToSkipIsSetManually { get; private set; }

        private int itemsToSkip;

        public int ItemsToSkip
        {
            get
            {
                {
                    if (ItemsToSkipIsSetManually)
                    {
                        return itemsToSkip;
                    }
                    else
                    {
                        return (this.CurrentPage - 1) * this.ItemsPerPage;
                    }
                }
            }
            set
            {
                if (!ItemsToSkipIsSetManually)
                {
                    itemsToSkip = value;
                }
            }
        }

        public EPaginationType PaginationType { get; set; }
        #endregion

        public PaginationInfo()
        {
            this.CurrentPage = 1;
            this.ItemsPerPage = CommonSettings.ItemsPerPage;
        }

        public PaginationInfo(int currentPage)
        {
            this.CurrentPage = currentPage <= 0 ? 1 : currentPage;
            this.ItemsPerPage = CommonSettings.ItemsPerPage;
        }

        public PaginationInfo(int currentPage, EPaginationType paginationType)
        {
            this.CurrentPage = currentPage <= 0 ? 1 : currentPage;
            this.ItemsPerPage = CommonSettings.ItemsPerPage;
            PaginationType = paginationType;
        }

        /// <summary>
        /// Inittializes contructor for specific with item per page
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        public PaginationInfo(int currentPage, int itemsPerPage)
        {
            this.CurrentPage = currentPage <= 0 ? 1 : currentPage;
            this.ItemsPerPage = itemsPerPage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationInfo"/> class.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        public PaginationInfo(string currentPage)
        {
            //// set current page
            try
            {
                this.CurrentPage = int.Parse(currentPage);
            }
            catch
            {
                this.CurrentPage = 1;
            }
            //// set number item on one page of list
            this.ItemsPerPage = CommonSettings.ItemsPerPage;
        }
    }
}
