using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    public interface IFilterBiddingNewsBL
    {
        IEnumerable<ConstructionVM> ReadAllContructionByUserId(long? userId);
    }
}
