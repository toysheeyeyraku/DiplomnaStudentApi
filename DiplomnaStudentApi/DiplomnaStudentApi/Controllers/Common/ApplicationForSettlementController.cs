using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Services;
using DiplomnaStudentApi.Student.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Controllers.Student
{
    [Route("[controller]")]
    public class ApplicationForSettlementController : ApiController
    {
        private IApplicationForSettlementService applicationForSettlementService;
        private IWordGeneratorService wordGeneratorService;

        public ApplicationForSettlementController(IApplicationForSettlementService applicationForSettlementService, IWordGeneratorService wordGeneratorService)
        {
            this.applicationForSettlementService = applicationForSettlementService;
            this.wordGeneratorService = wordGeneratorService;
        }

        [Authorize(Roles = "student")]
        [Route("/student/applicationSettlement")]
        [HttpPost]
        public string AddSettlement([FromBody] ApplicationForSettlementDto applicationForSettlementDto)
        {
            applicationForSettlementService.AddApplicationForSettlement(GetUserId(), applicationForSettlementDto.StartYear);
            return "ok";
        }

        [Authorize(Roles = "admin,comendant,dean,passportHolder")]
        [Route("/common/applicationSettlement")]
        [HttpGet]
        public List<ApplicationForSettlementDto> GetApplicationSettlement([FromQuery(Name = "studentId")] string studentId)
        {
            return applicationForSettlementService.GetApplicationForSettlementDtos(studentId);
        }

        [Authorize(Roles = "student")]
        [Route("/student/applicationSettlement")]
        [HttpGet]
        public List<ApplicationForSettlementDto> GetApplicationSettlementStudent()
        {
            return applicationForSettlementService.GetApplicationForSettlementDtos(GetUserId());
        }

        [Authorize(Roles = "dean")]
        [Route("/dean/applicationSettlement/sign")]
        [HttpPost]
        public string DeanSignApplicationSettlement([FromQuery(Name = "applicationSettlementId")] string applicationSettlementId)
        {
            applicationForSettlementService.CheckApplicationForSettlementDean(applicationSettlementId, GetUserId());
            return "ok";
        }

        [Authorize(Roles = "comendant")]
        [Route("/comendant/applicationSettlement/sign")]
        [HttpPost]
        public string ComendantSignApplicationSettlement([FromQuery(Name = "applicationSettlementId")] string applicationSettlementId)
        {
            applicationForSettlementService.CheckApplicationForSettlementCommendant(applicationSettlementId, GetUserId());
            return "ok";
        }

        [Route("/common/generateWord")]
        [HttpGet]
        public FileStreamResult GenereteWord([FromQuery(Name = "applicationSettlementId")] string applicationSettlementId)
        {

            MemoryStream memoryStream = wordGeneratorService.GenerateWordApplicationForSettlement(applicationSettlementId);
            var file = File(memoryStream, "application/octet-stream", "myfile.docx");
            return file;
            
        }
    }
}
