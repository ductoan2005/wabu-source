using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.Infrastructure
{
    public class DbExecutionResult
    {
        public int AffectedRows { get; internal set; }
        public EDbExecutionResult ResultType { get; internal set; }
        public Exception Exception { get; internal set; }
    }

    public enum EDbExecutionResult
    {
        None,
        Normal,
        EntityNotExist,
        DuplicatePrimaryKey,
        EntityIsUse,
        CommonError
    }
}
