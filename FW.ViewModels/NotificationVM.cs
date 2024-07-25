using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.ViewModels
{
    public class ListNotificationVM
    {
        public List<NotificationVM> ListNotify { get; set; }
    }

    public class NotificationVM
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
