using DiplomnaStudentApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IPaymentService
    {
        void AddPayment(PaymentDto paymentDto, string userId);
        List<PaymentDto> GetPayments(string userId);
    }
}
