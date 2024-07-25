using AutoMapper;
using FW.BusinessLogic.Interfaces;
using FW.Data.RepositoryInterfaces;
using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BusinessLogic.Implementations
{
    public class NotificationBL : BaseBL, INotificationBL
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationBL(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public List<NotificationVM> GetAllNotifyByUserId(long? userId)
        {
            var notifyList = _notificationRepository.GetMany(notif=>notif.UserId == userId).ToList();
            var notifyListVM = Mapper.Map<List<Notification>, List<NotificationVM>>(notifyList);
            return notifyListVM;
        }
    }
}
