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

    public class ApiController : ControllerBase
    {
        public byte [] GetImageFromRequestBytes()
        {

            IFormFile formFile = Request.Form.Files[0];
            Stream stream = formFile.OpenReadStream();
            byte[] blob = new byte[stream.Length];
            stream.Read(blob, 0, (int)stream.Length);
            return blob;

        }

        public string GetUserId()
        {
            string userId = User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            return userId;
        }
    }
}
