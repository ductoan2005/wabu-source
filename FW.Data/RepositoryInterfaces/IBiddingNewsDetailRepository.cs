using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;
using FW.Models;
using FW.ViewModels.BiddingNews;

namespace FW.Data.RepositoryInterfaces
{
    public interface IBiddingNewsDetailRepository : IRepository<BiddingDetail, long?>
    {
        #region Read

        bool CheckContructionByProfileId(long? profileId);
        
        #endregion

    }
}
