
using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Services;
using DiplomnaStudentApi.Student.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Controllers.Student
{
    public class StudentPaymentController: ApiController
    {
        private IPaymentService paymentService;
        public StudentPaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [Authorize(Roles = "student")]
        [Route("student/payment")]
        [HttpPost]
        public string AddPayment()
        {
            string userId = base.GetUserId();
            byte[] paymentImage = base.GetImageFromRequestBytes();
            PaymentDto paymentDto = new PaymentDto();
            paymentDto.Date = DateTime.Now.ToString();
            paymentDto.PaymentImage = paymentImage;
            paymentDto.UserId = userId;
            paymentService.AddPayment(paymentDto, userId);
            return "ok";
        }

        [Authorize(Roles = "student")]
        [Route("student/payment")]
        [HttpGet]
        public List<PaymentDto> GetPayments()
        {
            string userId = base.GetUserId(); 
            return paymentService.GetPayments(userId);
        }
    }
}
