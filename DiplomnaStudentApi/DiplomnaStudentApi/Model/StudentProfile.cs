using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Model
{
    public class StudentProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Room { get; set; }
        public string Group { get; set; }
        public string Faculty { get; set; }
        public string PassportNumber { get; set; }
        public string PassportGivenDate { get; set; }
        public string DateBirth { get; set; }
        public string Course { get; set; }

        public byte[] ProfileImage { get; set; }
        public byte[] SignImage { get; set; }

    }
}
