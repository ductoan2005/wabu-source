using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common.Helpers
{
    public class CommonExceptions : Exception
    {
        public CommonExceptions(string msgError)
            : base(msgError)
        {

        }
    }
}
