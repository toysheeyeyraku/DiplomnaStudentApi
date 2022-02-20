using DiplomnaStudentApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.DTO
{
    public class ResidenceAgreementDto
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string ComendantId { get; set; }
        public bool IsActive { get; set; }
        public List<ResidenceAgreementSignature> YearsSignatures { get; set; }
        public int LastYear { get; set; }

    }
}
