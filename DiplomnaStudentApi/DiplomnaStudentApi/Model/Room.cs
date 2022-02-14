using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Model
{
    public class Room
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] RoomImage { get; set; }
        public int Capacity { get; set; }
        public List<string> UserIds { get; set; }

    }
}
