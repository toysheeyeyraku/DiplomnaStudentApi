using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.DTO
{
    public class NotificationDto
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string Authror { get; set; }
    }
}
