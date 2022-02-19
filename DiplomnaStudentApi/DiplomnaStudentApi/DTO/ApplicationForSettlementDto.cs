using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.DTO
{
    public class ApplicationForSettlementDto
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string DeanId { get; set; }
        public bool DeanIdChecked { get; set; }
        public string ComendantId { get; set; }
        public bool ComendantChecked { get; set; }
        public string Date { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}
