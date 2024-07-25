using FW.Data.Infrastructure;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;
using FW.ViewModels;

namespace FW.Data.EFs.Repositories
{
    public class BiddingNewsRegistrationRepository : RepositoryBase<BiddingNews, long?>, IBiddingNewsRegistrationRepository
    {
        public BiddingNewsRegistrationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }
     }
}
