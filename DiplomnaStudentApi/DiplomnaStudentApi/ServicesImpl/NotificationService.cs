using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class NotificationService: INotificationService
    {
        INotificationManager notificationManager;
        public NotificationService(INotificationManager notificationManager)
        {
            this.notificationManager = notificationManager;
        }

        public List<NotificationDto> GetAllNotifications()
        {
            List<Notification> notifications = notificationManager.GetAll().Reverse().ToList();
            List<NotificationDto> notificationDtos = new List<NotificationDto>();
            foreach (var notification in notifications)
            {
                NotificationDto dto = new NotificationDto();
                dto.Author = notification.Author;
                dto.Body = notification.Body;
                dto.Date = notification.Date;
                dto.Header = notification.Header;
                notificationDtos.Add(dto);
            }
            return notificationDtos;
        }
    
        public void AddNotification(NotificationDto notificationDto)
        {
            Notification notification = new Notification();
            notification.Author = notificationDto.Author;
            notification.Body = notificationDto.Body;
            notification.Date = notificationDto.Date;
            notification.Header = notificationDto.Header;
            notification.Id = new ObjectId(DateTime.Now, 1, 1, 1).ToString();
            notificationManager.Add(notification);
        }
        
    }
}
