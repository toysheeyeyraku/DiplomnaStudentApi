using DiplomnaStudentApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IStudentProfileService
    {
        void UpdateStudentProfile(StudentProfileDto studentUpdateRequest, string userId);
        void UpdateStudentProfileImage(byte[] blobImage, string userId);
        StudentProfileDto GetStudentProfile(string userId);
    }
}
