using FW.Data.Infrastructure;
using FW.Data.Infrastructure.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.EFs.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification, long?>, INotificationRepository
    {
        public NotificationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
