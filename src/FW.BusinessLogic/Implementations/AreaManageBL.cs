using FW.BusinessLogic.Interfaces;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FW.BusinessLogic.Implementations
{
    public class AreaManageBL : BaseBL, IAreaManageBL
    {
        private readonly IAreaManageRepository _areaManageRepository;

        public AreaManageBL(IAreaManageRepository areaManageRepository)
        {
            _areaManageRepository = areaManageRepository;
        }

        public IEnumerable<SelectListItem> GetAllArea()
        {
            return _areaManageRepository.GetAll().Where(a => a.IsDeleted != true)
                .Select(a =>
            new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.AreaName
            }).Distinct();
        }
    }
}
