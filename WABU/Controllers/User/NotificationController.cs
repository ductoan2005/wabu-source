using FW.BusinessLogic.Interfaces;
using FW.Models;
using FW.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WABU.Hubs;
using WABU.Utilities;

namespace WABU.Controllers.User
{
    public class NotificationController : Controller
    {
        private readonly INotificationBL _notificationBL;

        public NotificationController(INotificationBL notificationBL)
        {
            _notificationBL = notificationBL;
        }

        // GET: Notification
        public ActionResult GetNotifications()
        {
            try
            {
                var session = SessionObjects.UserProfile;
                if (session != null)
                {
                    var notifyList = _notificationBL.GetAllNotifyByUserId(session.UserID);
                    ViewBag.NotifyList = notifyList;
                    ViewBag.NotifyCount = notifyList.Count;
                    //return PartialView(@"~\Views\Partial_view\_PV_Notification.cshtml", notifyList);
                }

                return PartialView(@"~\Views\Partial_view\_PV_Notification.cshtml");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                SignalRNotificationHub.SendMessages();
            }
        }
    }
}