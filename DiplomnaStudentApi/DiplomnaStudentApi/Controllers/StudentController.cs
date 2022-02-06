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

namespace DiplomnaStudentApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private IStudentProfileManager studentProfileManager = new StudentProfileManager();
        //[Authorize(Roles = "student")]
        [Route("/student/profile")]
        [HttpGet]
        public string Get()
        {
            var userID = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            StudentProfile studentProfile = studentProfileManager.GetFirstOrDefault(filter => filter.ThridName.Equals("Bogdan"));

            return "get";
        }

        [Route("/student/profileImage")]
        [HttpGet]
        public byte[] profileImage()
        {
            
            var userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            if (studentProfile == null)
            {
                return null;
            }
            return studentProfile.Image;
        }

        [Authorize(Roles = "student")]
        [Route("/student/profile/update")]
        [HttpPost]
        [RequestSizeLimit(40000000)]
        public string updateProfile()
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
    }
}
