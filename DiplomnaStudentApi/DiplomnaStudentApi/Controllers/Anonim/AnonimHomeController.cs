using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
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
        private INotificationService notificationService;

        public AnonimHomeController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [Route("/home")]
        [HttpGet]
        public List<NotificationDto> GetNotifications()
        {
            List<NotificationDto> notifications = notificationService.GetAllNotifications();
            return notifications;
        }
    }
}
