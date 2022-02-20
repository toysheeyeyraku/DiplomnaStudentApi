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
    public class ResidentAgreementController : ApiController
    {
        private IResidenceAgreementService residentAgreementService;

        public ResidentAgreementController(IResidenceAgreementService residentAgreementService)
        {
            this.residentAgreementService = residentAgreementService;
        }

        [Authorize(Roles = "student")]
        [Route("/student/residenceAgreement/add")]
        [HttpPost]
        public string createResidenceAgreementDto([FromBody] ResidenceAgreementDto residenceAgreementDto)
        {
            string userId = base.GetUserId();
            residentAgreementService.GenereNewResidenceAgreement(userId, residenceAgreementDto.LastYear);
            return "ok";
        }

        [Authorize(Roles = "student")]
        [Route("/student/residenceAgreement")]
        [HttpGet]
        public List<ResidenceAgreementDto> GetResidentAgrrementStudent()
        {
            string userId = base.GetUserId();
            return residentAgreementService.GetResidenceAgreements(userId);
        }

        [Authorize(Roles = "comendant")]
        [Route("/common/residenceAgreement")]
        [HttpGet]
        public ResidenceAgreementDto GetResidentAgrrement([FromQuery(Name = "studentId")] string studentId)
        {
            return residentAgreementService.GetResidenceAgreement(studentId);
        }

        [Authorize(Roles = "comendant")]
        [Route("/common/residenceAgreement/sign")]
        [HttpPost]
        public string ResidentAgrrementSign([FromQuery(Name = "studentId")] string studentId)
        {
            string comendantId = GetUserId();
            residentAgreementService.SignResidenceAgreement(studentId, comendantId);
            return "ok";
        }

        [Authorize(Roles = "comendant")]
        [Route("/common/residenceAgreement/deactivate")]
        [HttpPost]
        public string ResidentAgrrementDeactivate([FromQuery(Name = "residenceAgreementId")] string residenceAgreementId)
        {
            //string comendantId = GetUserId();
            //residentAgreementService.SignResidenceAgreement(studentId, comendantId);
            return "ok";
        }

    }
}
