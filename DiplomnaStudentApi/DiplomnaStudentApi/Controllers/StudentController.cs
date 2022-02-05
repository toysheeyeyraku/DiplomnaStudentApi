using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            //var userID = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            StudentProfile studentProfile = studentProfileManager.GetFirstOrDefault(filter => filter.ThridName.Equals("Bogdan"));

            return "get";
        }
    }
}
