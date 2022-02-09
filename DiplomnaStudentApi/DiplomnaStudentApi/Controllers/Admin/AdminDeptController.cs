using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomnaStudentApi.Admin.Controller
{

    [ApiController]
    [Route("[controller]")]
    public class AdminDeptController : ControllerBase
    {
        private IDebtService debtService;
        public AdminDeptController(IDebtService debtService)
        {
            this.debtService = debtService;
        }

        [Authorize(Roles = "admin")]
        [Route("admin/debt")]
        [HttpPost]
        public string UpdateDebt([FromBody] DebtDto debtDto, [FromQuery(Name = "userId")] string userId)
        {

            debtService.UpdateDebt(debtDto, userId);
            return "ok";

        }

        [Authorize(Roles = "admin")]
        [Route("admin/debt")]
        [HttpGet]
        public DebtDto GetDebt([FromQuery(Name = "userId")] string userId)
        {

            return debtService.GetDebtDto(userId);
        }



    }
}
