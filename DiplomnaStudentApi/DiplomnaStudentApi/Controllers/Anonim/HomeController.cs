using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Web.Helpers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Anonim.Controller
{

    [ApiController]
    [Route("[controller]")]
    public class AnonimHomeController : ControllerBase
    {
        private INotificationManager notificationManager = new NotificationManager();

        [Route("/home")]
        [HttpGet]
        public List<NotificationDto> GetNotifications()
        {
            List<Notification> notifications = notificationManager.GetAll().ToList();
            List<NotificationDto> notificationDtos = new List<NotificationDto>();
            foreach(var notification in notifications)
            {
                NotificationDto dto = new NotificationDto();
                dto.Authror = notification.Authror;
                dto.Body = notification.Body;
                dto.Date = notification.Date;
                dto.Header = notification.Header;
                notificationDtos.Add(dto);
            }
            return notificationDtos;

        }

        [Authorize(Roles = "admin")]
        [Route("/home")]
        [HttpPost]
        public string AddNotification([FromBody] NotificationDto notificationDto)
        {
            Notification notification = new Notification();
            notification.Authror = notificationDto.Authror;
            notification.Body = notificationDto.Body;
            notification.Date = notificationDto.Date;
            notification.Header = notificationDto.Header;
            notification.Id = new ObjectId(DateTime.Now, 1, 1, 1).ToString();
            notificationManager.Add(notification);
            return "ok";

        }



    }
}
