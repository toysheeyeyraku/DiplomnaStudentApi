using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IApplicationForSettlementService
    {
        void AddApplicationForSettlement(string studentId, int yearStart);
        void CheckApplicationForSettlementDean(string applicationForSettlementId, string deanId);
        void CheckApplicationForSettlementCommendant(string applicationForSettlementId, string commendantId);
        List<ApplicationForSettlementDto> GetApplicationForSettlementDtos(string studentId);
        ApplicationForSettlement GetApplicationForSettlement(string applicationForSettlementId);

    }
}
