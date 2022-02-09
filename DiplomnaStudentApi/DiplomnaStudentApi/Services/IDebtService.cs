using DiplomnaStudentApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IDebtService
    {
        void UpdateDebt(DebtDto debtDto, string userId);
        DebtDto GetDebtDto(string userId);
    }
}
