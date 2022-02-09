using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class DebtService: IDebtService
    {
        private IDebtManager debtManager;
        public DebtService(IDebtManager debtManager)
        {
            this.debtManager = debtManager;
        }

        public void UpdateDebt(DebtDto debtDto, string userId)
        {
            Debt debt = debtManager.GetById(userId);
            bool isExisted = true;
            if (debt == null)
            {
                debt = new Debt();
                debt.Id = userId;
                isExisted = false;
            }

            debt.MoneyDept = debtDto.MoneyDept;
            if (isExisted)
            {
                debtManager.Update(userId, debt);
            }
            else
            {
                debtManager.Add(debt);
            }
        }
    
        public DebtDto GetDebtDto(string userId)
        {
            DebtDto debtDto = new DebtDto();
            Debt debt = debtManager.GetById(userId);

            if (debt == null)
            {
                debtDto.MoneyDept = 0;
            }
            else
            {
                debtDto.MoneyDept = debt.MoneyDept;
            }
            return debtDto;
        }
    }
}
