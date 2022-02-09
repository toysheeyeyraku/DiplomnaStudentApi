using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Model
{
    public class Payment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public byte [] PaymentImage { get; set; }
        public string Date { get; set; }

    }
}
