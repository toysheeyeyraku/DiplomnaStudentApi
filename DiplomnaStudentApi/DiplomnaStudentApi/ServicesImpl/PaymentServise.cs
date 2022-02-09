using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class PaymentServise: IPaymentService
    {
        private IPaymentManager paymentManager;
        public PaymentServise(IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
        }


        public void AddPayment(PaymentDto paymentDto, string userId)
        {
            Payment payment = new Payment();
            payment.Id = new ObjectId(DateTime.Now, 1, 1, 1).ToString();
            payment.UserId = userId;
            payment.PaymentImage = paymentDto.PaymentImage;
            payment.Date = paymentDto.Date;
            paymentManager.Add(payment);
        }


        public List<PaymentDto> GetPayments(string userId)
        {
            return paymentManager.GetAll(filter => filter.UserId.Equals(userId)).Select(m => new PaymentDto(m.Id, m.UserId, m.PaymentImage, m.Date)).ToList();
        }

    }
}
