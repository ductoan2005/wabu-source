using FW.Common.Helpers;
using FW.Data.Infrastructure;
using FW.Resources;

namespace FW.BusinessLogic
{
    public class BaseBL
    {
        public void CheckDbExecutionResultAndThrowIfAny(DbExecutionResult result)
        {
            switch (result.ResultType) //return err
            {
                case EDbExecutionResult.DuplicatePrimaryKey:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_EXIST_RECORD);
                case EDbExecutionResult.CommonError:
                    throw new CommonExceptions(CommonResource.MSG_ERROR_SYSTEM);
            }
        }
    }
}
