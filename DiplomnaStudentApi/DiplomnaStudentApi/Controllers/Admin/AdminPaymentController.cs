
using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Services;
using DiplomnaStudentApi.Student.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Admin.Controller
{
    public class AdminPaymentController : ApiController
    {
        private IPaymentService paymentService;
        public AdminPaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [Authorize(Roles = "admin")]
        [Route("admin/payment")]
        [HttpGet]
        public List<PaymentDto> GetPayments([FromQuery(Name = "userId")] string userId)
        {
            return paymentService.GetPayments(userId);
        }
    }
}
