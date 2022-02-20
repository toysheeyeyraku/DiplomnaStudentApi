using DiplomnaStudentApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IResidenceAgreementService
    {
        void GenereNewResidenceAgreement(string studentId, int studyYear);
        void SignResidenceAgreement(string studentId, string comendantId);
        List<ResidenceAgreementDto> GetResidenceAgreements(string studentId);
        ResidenceAgreementDto GetResidenceAgreement(string studentId);

    }
}
