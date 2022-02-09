using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomnaStudentApi.Admin.Controller
{

    [ApiController]
    [Route("[controller]")]
    public class AdminHomeController : ControllerBase
    {
        private INotificationService notificationService;

        public AdminHomeController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [Authorize(Roles = "admin")]
        [Route("/home")]
        [HttpPost]
        public string AddNotification([FromBody] NotificationDto notificationDto)
        {
            notificationService.AddNotification(notificationDto);
            return "ok";

        }



    }
}
