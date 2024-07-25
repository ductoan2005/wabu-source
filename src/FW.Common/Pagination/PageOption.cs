using FW.Common.Enum;
using FW.Common.Pagination.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common.Pagination
{
    public class PageOption : IPaginationInfo
    {
        #region Properties

        public int ItemsPerPage { get; set; }

        int _totalItems = 0;
        public int TotalItems
        {
            get
            {
                return _totalItems;
            }
            set
            {
                _totalItems = value;
                if (_totalItems <= this.ItemsPerPage * (this.CurrentPage - 1))
                    this.CurrentPage -= 1;
            }
        }

        public int CurrentPage { get; set; }

        private int itemsToSkip;
        public int ItemsToSkip
        {
            get
            {
                return (this.CurrentPage - 1) * this.ItemsPerPage;
            }
            set
            {
                itemsToSkip = value;
            }
        }

        private int totalPages;
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
                    return (int)Math.Ceiling((decimal)this.TotalItems / this.ItemsPerPage);
                }
            }
            set
            {
                totalPages = value;
            }
        }

        public EPaginationType PaginationType { get; set; }
        #endregion

        public PageOption(EPaginationType paginationType = EPaginationType.HyperLink)
        {
            this.CurrentPage = 1;
            this.ItemsPerPage = 10;
            this.PaginationType = paginationType;
        }

        public PageOption(string currentPage)
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
            this.ItemsPerPage = 10;
        }
    }
}
