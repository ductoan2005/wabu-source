using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IBiddingNewsAbilityHRBL
    {
        /// <summary>
        /// GetJobPositionKeyWord
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetJobPositionKeyWord(string term);
    }
}
