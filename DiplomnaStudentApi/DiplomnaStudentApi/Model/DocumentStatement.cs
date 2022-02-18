using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Model
{
    public class DocumentStatement
    {
        public string Id { get; set; }
        public string Student { get; set; }
        public bool IsActive { get; set; }
        public int Year { get; set; }
        public string Date { get; set; }
    }
}
