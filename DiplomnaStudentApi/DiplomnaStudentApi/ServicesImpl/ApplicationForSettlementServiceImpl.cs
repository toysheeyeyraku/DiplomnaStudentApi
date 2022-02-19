using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using DiplomnaStudentApi.Utils;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class ApplicationForSettlementServiceImpl : IApplicationForSettlementService
    {
        private IApplicationForSettlementManager applicationForSettlementManager;
        private ILogger<ApplicationForSettlementServiceImpl> logger;

        public ApplicationForSettlementServiceImpl(IApplicationForSettlementManager applicationForSettlementManager, ILogger<ApplicationForSettlementServiceImpl> logger)
        {
            this.applicationForSettlementManager = applicationForSettlementManager;
            this.logger = logger;
        }
        
        public void AddApplicationForSettlement(string studentId, int yearStart)
        {
            
            if (IsApplicationSettlementExistForStudentWithYearStart(studentId, yearStart))
            {
                logger.LogWarning($"Student {studentId} already exist with this year {yearStart} in db ");
                throw new Exception($"Student {studentId} already exist with this year {yearStart} in db ");
            }

            ApplicationForSettlement settlement = new ApplicationForSettlement();
            settlement.Date = DateTime.Now.ToString();
            settlement.Id = new ObjectId(DateTime.Now, 1, 1, 1).ToString();
            settlement.StudentId = studentId;
            settlement.StartYear = yearStart;
            settlement.EndYear = yearStart + 1;
            applicationForSettlementManager.Add(settlement);
        }

        public void CheckApplicationForSettlementDean(string applicationForSettlementId, string deanId)
        {
            ApplicationForSettlement applicationForSettlement = applicationForSettlementManager.GetById(applicationForSettlementId);
            if (applicationForSettlement == null)
            {
                logger.LogWarning($"applicationForSettlementId not exist {applicationForSettlementId}");
                throw new Exception($"applicationForSettlementId not exist {applicationForSettlementId}");
            }

            applicationForSettlement.DeanIdChecked = true;
            applicationForSettlement.DeanId = deanId;
            applicationForSettlementManager.Update(applicationForSettlementId, applicationForSettlement);
        }

        public void CheckApplicationForSettlementCommendant(string applicationForSettlementId, string commendantId)
        {
            ApplicationForSettlement applicationForSettlement = applicationForSettlementManager.GetById(applicationForSettlementId);
            if (applicationForSettlement == null)
            {
                logger.LogWarning($"applicationForSettlementId not exist {applicationForSettlementId}");
                throw new Exception($"applicationForSettlementId not exist {applicationForSettlementId}");
            }

            applicationForSettlement.ComendantChecked = true;
            applicationForSettlement.ComendantId = commendantId;
            applicationForSettlementManager.Update(applicationForSettlementId, applicationForSettlement);
        }

        public List<ApplicationForSettlementDto> GetApplicationForSettlementDtos(string studentId)
        {
            List<ApplicationForSettlementDto> ans = applicationForSettlementManager.GetAll(filter => filter.StudentId.Equals(studentId))
                    .Select(m => MapperUtil.CopyAToB<ApplicationForSettlement, ApplicationForSettlementDto>(m)).ToList();
            return ans;

        }

        public ApplicationForSettlement GetApplicationForSettlement(string applicationForSettlementId)
        {
            return applicationForSettlementManager.GetById(applicationForSettlementId);
        }


        private bool IsApplicationSettlementExistForStudentWithYearStart(string studentId, int yearStart)
        {
            var isExist = applicationForSettlementManager.GetFirstOrDefault(filter => (studentId.Equals(filter.StudentId) && yearStart == filter.StartYear));
            return (isExist != null);
        }
    }
}
