using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
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
    public class StudentProfileController : ControllerBase
    {
        private IStudentProfileManager studentProfileManager = new StudentProfileManager();

        [Authorize(Roles = "student")]
        [Route("/student/profile")]
        [HttpGet]
        public StudentProfileDto StudentProfile()
        {

            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            if (studentProfile == null)
            {
                return new StudentProfileDto();
            }
            StudentProfileDto studentProfileDto = new StudentProfileDto();
            studentProfileDto.Email = studentProfile.Email;
            studentProfileDto.FirstName = studentProfile.FirstName;
            studentProfileDto.SecondName = studentProfile.SecondName;
            studentProfileDto.ThirdName = studentProfile.ThridName;
            studentProfileDto.Phone = studentProfile.Phone;
            studentProfileDto.ProfileImage = studentProfile.Image;
            return studentProfileDto;
        }

        [Authorize(Roles = "student")]
        [Route("/student/profileImage/update")]
        [HttpPost]
        [RequestSizeLimit(40000000)]
        public string UpdateProfileImage()
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            IFormFile formFile = Request.Form.Files[0];
            Stream stream = formFile.OpenReadStream();
            byte[] blob = new byte[stream.Length];
            stream.Read(blob, 0, (int)stream.Length);

            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            bool isExist = true;
            if (studentProfile == null)
            {
                studentProfile = new StudentProfile();
                studentProfile.Id = userId;
                isExist = false;
            }
            studentProfile.Image = blob;
            if (isExist)
            {
                studentProfileManager.Update(userId, studentProfile);
            }
            else
            {
                studentProfileManager.Add(studentProfile);
            }

            return "a";

        }


        [Authorize(Roles = "student")]
        [Route("/student/profile/update")]
        [HttpPost]
        public string UpdateProfile([FromBody] StudentProfileDto studentUpdateRequest)
        {
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            bool isExist = true;
            if (studentProfile == null)
            {
                isExist = false;
                studentProfile = new StudentProfile();
                studentProfile.Id = userId;
            }

            studentProfile.Email = studentUpdateRequest.Email;
            studentProfile.FirstName = studentUpdateRequest.FirstName;
            studentProfile.SecondName = studentUpdateRequest.SecondName;
            studentProfile.ThridName = studentUpdateRequest.ThirdName;
            studentProfile.Phone = studentUpdateRequest.Phone;
            if (isExist)
            {
                studentProfileManager.Update(userId, studentProfile);
            }
            else
            {
                studentProfileManager.Add(studentProfile);
            }
            return "ok";
        }

    }
}
