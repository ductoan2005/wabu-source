using FW.BusinessLogic.Interfaces;
using FW.Common.Helpers;
using FW.Common.Utilities;
using FW.Resources;
using FW.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WABU.Utilities;

namespace WABU.Controllers
{
    public class ContractorController : BaseController
    {
        private readonly ICompanyBL _companyBL;
        private readonly ICompanyStaffBL _companyStaffBL;

        public ContractorController(ICompanyBL CompanyBL, ICompanyStaffBL companyStaffBL)
        {
            _companyBL = CompanyBL;
            _companyStaffBL = companyStaffBL;
        }
        // GET: Contractor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ContractorDetail()
        {
            return View();
        }

        public ActionResult ContractorInformation(long id)
        {
            var contractorInfor = new ContractorInformationVM();
            contractorInfor = _companyBL.GetContractorInformation(id);
            var companyStaffs = _companyStaffBL.GetCompanyStaffByCompanyId(contractorInfor.Id.Value);
            contractorInfor.Staffs = companyStaffs.ToArray();

            return PartialView(contractorInfor);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateContractorInformation(CompanyVM viewModel)
        {
            var methodName = SysLogger.GetMethodFullName();
            try
            {
                viewModel.UserId = SessionObjects.UserProfile.UserID;
                await _companyBL.UpdateContractorInformation(viewModel);
                return Json(new { succeed = CommonConstants.STR_ZERO });
            }
            catch (Exception ex)
            {
                return ExportMsgExcaption(ex);
            }
            finally
            {
                SysLogger.Info(string.Format(CommonResource.LoggerEndMethod, methodName));
            }
        }
    }
}