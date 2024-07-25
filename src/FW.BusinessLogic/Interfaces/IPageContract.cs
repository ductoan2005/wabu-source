using System.Collections.Generic;
using FW.ViewModels;

namespace FW.BusinessLogic.Interfaces
{
    public interface IPageContract
    {
        IEnumerable<PageContractVM> ReadPagingPageContract();
    }
}
