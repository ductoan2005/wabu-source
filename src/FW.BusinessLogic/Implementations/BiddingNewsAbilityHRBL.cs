using FW.BusinessLogic.Interfaces;
using FW.Data.RepositoryInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class BiddingNewsAbilityHRBL : BaseBL, IBiddingNewsAbilityHRBL
    {
        #region Constants

        internal const string ORDER_BY_DEFAULT = "";

        #endregion

        #region Fields

        private readonly IBiddingNewsAbilityHRRepository _biddingNewsAbilityHRRepository;

        #endregion

        #region Ctor

        public BiddingNewsAbilityHRBL(IBiddingNewsAbilityHRRepository biddingNewsAbilityHRRepository)
        {
            _biddingNewsAbilityHRRepository = biddingNewsAbilityHRRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// GetJobPositionKeyWord
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetJobPositionKeyWord(string term) => _biddingNewsAbilityHRRepository.GetJobPositionKeyWord(term);

        #endregion

    }
}
