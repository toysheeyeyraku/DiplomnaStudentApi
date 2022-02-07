using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.DTO
{
    public class StudentProfileDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte [] ProfileImage { get; set; }
    }
}
