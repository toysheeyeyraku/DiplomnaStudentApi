using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Model
{
    public class Notification
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }
    }
}
