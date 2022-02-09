using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.DTO
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public byte[] PaymentImage { get; set; }
        public string Date { get; set; }

        public PaymentDto()
        {

        }

        public PaymentDto(string id, string userId, byte [] paymentImage, string date)
        {
            Id = id;
            UserId = userId;
            PaymentImage = paymentImage;
            Date = date;
        }
    }
}
