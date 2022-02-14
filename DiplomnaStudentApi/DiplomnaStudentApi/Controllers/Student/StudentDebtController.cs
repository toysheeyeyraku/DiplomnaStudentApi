using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomnaStudentApi.Student.Controller
{

    [Route("[controller]")]
    public class StudentDeptController : ApiController
    {
        private IDebtService debtService;
        public StudentDeptController(IDebtService debtService)
        {
            this.debtService = debtService;
        }

        [Authorize(Roles = "student")]
        [Route("/student/debt")]
        [HttpGet]
        public DebtDto GetDebt()
        {
            string userId = base.GetUserId();
            return debtService.GetDebtDto(userId);
        }



    }
}
