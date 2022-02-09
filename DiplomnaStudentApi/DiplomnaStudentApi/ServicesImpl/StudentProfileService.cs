using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Repository;
using DiplomnaStudentApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class StudentProfileService: IStudentProfileService
    {
        IStudentProfileManager studentProfileManager;
        public StudentProfileService(IStudentProfileManager studentProfileManager)
        {
            this.studentProfileManager = studentProfileManager;
        }

        public void UpdateStudentProfile(StudentProfileDto studentUpdateRequest, string userId)
        {
            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            bool isExist = true;
            if (studentProfile == null)
            {
                isExist = false;
                studentProfile = new StudentProfile();
                studentProfile.Id = userId;
            }

            studentProfile.Email = studentUpdateRequest.Email;
            studentProfile.FirstName = studentUpdateRequest.FirstName;
            studentProfile.SecondName = studentUpdateRequest.SecondName;
            studentProfile.ThridName = studentUpdateRequest.ThirdName;
            studentProfile.Phone = studentUpdateRequest.Phone;
            if (isExist)
            {
                studentProfileManager.Update(userId, studentProfile);
            }
            else
            {
                studentProfileManager.Add(studentProfile);
            }
        }
    
        public void UpdateStudentProfileImage(byte [] blobImage, string userId)
        {
            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            bool isExist = true;
            if (studentProfile == null)
            {
                studentProfile = new StudentProfile();
                studentProfile.Id = userId;
                isExist = false;
            }
            studentProfile.Image = blobImage;
            if (isExist)
            {
                studentProfileManager.Update(userId, studentProfile);
            }
            else
            {
                studentProfileManager.Add(studentProfile);
            }
        }
        
        public StudentProfileDto GetStudentProfile(string userId)
        {
            StudentProfile studentProfile = studentProfileManager.GetById(userId);
            if (studentProfile == null)
            {
                return new StudentProfileDto();
            }
            StudentProfileDto studentProfileDto = new StudentProfileDto();
            studentProfileDto.Email = studentProfile.Email;
            studentProfileDto.FirstName = studentProfile.FirstName;
            studentProfileDto.SecondName = studentProfile.SecondName;
            studentProfileDto.ThirdName = studentProfile.ThridName;
            studentProfileDto.Phone = studentProfile.Phone;
            studentProfileDto.ProfileImage = studentProfile.Image;
            return studentProfileDto;
        }
    }
}
