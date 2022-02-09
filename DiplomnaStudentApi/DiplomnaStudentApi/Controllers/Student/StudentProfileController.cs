﻿using DiplomnaStudentApi.DTO;
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
    public class StudentProfileController : ApiController
    {
        private IStudentProfileService studentProfileService;

        public StudentProfileController(IStudentProfileService studentProfileService)
        {
            this.studentProfileService = studentProfileService;
        }


        [Authorize(Roles = "student")]
        [Route("/student/profile")]
        [HttpGet]
        public StudentProfileDto StudentProfile()
        {
            string userId = base.GetUserId();
            StudentProfileDto studentProfileDto = studentProfileService.GetStudentProfile(userId);
            return studentProfileDto;
        }

        [Authorize(Roles = "student")]
        [Route("/student/profileImage/update")]
        [HttpPost]
        [RequestSizeLimit(40000000)]
        public string UpdateProfileImage()
        {
            string userId = base.GetUserId();
            byte[] blobImage = base.GetImageFromRequestBytes();
            studentProfileService.UpdateStudentProfileImage(blobImage, userId);
            return "ok";

        }


        [Authorize(Roles = "student")]
        [Route("/student/profile/update")]
        [HttpPost]
        public string UpdateProfile([FromBody] StudentProfileDto studentUpdateRequest)
        {
            string userId = base.GetUserId();
            studentProfileService.UpdateStudentProfile(studentUpdateRequest, userId);
            return "ok";
        }

    }
}
