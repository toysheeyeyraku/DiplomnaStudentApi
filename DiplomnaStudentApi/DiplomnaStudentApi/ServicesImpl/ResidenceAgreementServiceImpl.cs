using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class ResidenceAgreementServiceImpl: IResidenceAgreementService
    {
        private IResidenceAgreementManager residenceAgreementManager;
        private ILogger<ResidenceAgreementServiceImpl> logger;

        public ResidenceAgreementServiceImpl(IResidenceAgreementManager residenceAgreementManager, ILogger<ResidenceAgreementServiceImpl> logger)
        {
            this.residenceAgreementManager = residenceAgreementManager;
            this.logger = logger;
        }

        public void GenereNewResidenceAgreement(string studentId, int studyYear)
        {
            ResidenceAgreement residenceAgreement = residenceAgreementManager.GetFirstOrDefault(filter => (studentId.Equals(filter.StudentId) && filter.IsActive == true));
            if (residenceAgreement != null)
            {
                return;
            }

            residenceAgreement = new ResidenceAgreement();
            residenceAgreement.Id = new ObjectId(DateTime.Now, 1, 1, 1).ToString();
            residenceAgreement.IsActive = true;
            residenceAgreement.YearsSignatures = new List<ResidenceAgreementSignature>();
            residenceAgreement.LastYear = studyYear;
            residenceAgreement.StudentId = studentId;
            residenceAgreementManager.Add(residenceAgreement);
        }

        public void SignResidenceAgreement(string studentId, string comendantId)
        {
            ResidenceAgreement residenceAgreement = residenceAgreementManager.GetFirstOrDefault(filter => (studentId.Equals(filter.StudentId) && filter.IsActive == true));
            int lastYearSigned = residenceAgreement.YearsSignatures.Last().Year;
            if (lastYearSigned == residenceAgreement.LastYear)
            {
                logger.LogWarning($"Studen {studentId} is already Signed");
                return;
            }
            ResidenceAgreementSignature signature = new ResidenceAgreementSignature();
            signature.ComendantId = comendantId;
            signature.Year = residenceAgreement.LastYear;
            signature.IsAssigned = true;
            residenceAgreement.YearsSignatures.Add(signature);

            if (residenceAgreement.ComendantId == null)
            {
                residenceAgreement.ComendantId = comendantId;
            }

        }
    
        public List<ResidenceAgreementDto> GetResidenceAgreements(string studentId)
        {
            return residenceAgreementManager.GetAll(filter => filter.StudentId.Equals(studentId)).Select(m=> Utils.MapperUtil.CopyAToB<ResidenceAgreement, ResidenceAgreementDto>(m)).ToList();
        }

        public ResidenceAgreementDto GetResidenceAgreement(string studentId)
        {
            ResidenceAgreement residentAgreement = residenceAgreementManager.GetFirstOrDefault(filter => (filter.StudentId.Equals(studentId) && filter.IsActive == true));
            if (residentAgreement == null)
            {
                return new ResidenceAgreementDto();
            }
            return Utils.MapperUtil.CopyAToB<ResidenceAgreement, ResidenceAgreementDto>(residentAgreement);
        }
    }
}
