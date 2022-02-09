using DiplomnaStudentApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface INotificationService
    {
        List<NotificationDto> GetAllNotifications();
        void AddNotification(NotificationDto notificationDto);
    }
}
