using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Student.Controller
{

    [ApiController]
    [Route("[controller]")]
    public class AdminStudentProfileController : ApiController
    {
        private IStudentProfileService studentProfileService;

        public AdminStudentProfileController(IStudentProfileService studentProfileService)
        {
            this.studentProfileService = studentProfileService;
        }

        [Authorize(Roles = "admin,comendant,dean,passportHolder")]
        [Route("/admin/student/profile")]
        [HttpGet]
        public StudentProfileDto StudentProfile([FromQuery(Name = "userId")] string userId)
        {
            StudentProfileDto studentProfileDto = studentProfileService.GetStudentProfile(userId);
            return studentProfileDto;
        }

        [Authorize(Roles = "admin,comendant,dean")]
        [Route("/admin/student/profileImage/update")]
        [HttpPost]
        [RequestSizeLimit(40000000)]
        public string UpdateProfileImage([FromQuery(Name = "userId")] string userId)
        {
            byte[] blobImage = base.GetImageFromRequestBytes();
            studentProfileService.UpdateStudentProfileImage(blobImage, userId);
            return "ok";

        }


        [Authorize(Roles = "admin,comendant,dean")]
        [Route("/admin/student/profile/update")]
        [HttpPost]
        public string UpdateProfile([FromBody] StudentProfileDto studentUpdateRequest, [FromQuery(Name = "userId")] string userId)
        {
            studentProfileService.UpdateStudentProfile(studentUpdateRequest, userId);
            return "ok";
        }

    }
}
