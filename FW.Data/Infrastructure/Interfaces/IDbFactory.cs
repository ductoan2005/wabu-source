using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        FWDbContext Init();
    }
}
