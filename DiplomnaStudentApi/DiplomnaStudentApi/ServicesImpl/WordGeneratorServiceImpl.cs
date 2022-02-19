using DiplomnaStudentApi.DTO;
using DiplomnaStudentApi.Model;
using DiplomnaStudentApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.ServicesImpl
{
    public class WordGeneratorServiceImpl: IWordGeneratorService
    {
        private IApplicationForSettlementService applicationForSettlementService;
        private IStudentProfileService studentProfileService;


        public WordGeneratorServiceImpl(IApplicationForSettlementService applicationForSettlementService, IStudentProfileService studentProfileService)
        {
            this.applicationForSettlementService = applicationForSettlementService;
            this.studentProfileService = studentProfileService;
        }


        public MemoryStream GenerateWordApplicationForSettlement(string applicationSettlementId)
        {
            string tempFilePath = Path.GetTempFileName();

            try
            {
                System.IO.File.Copy(@"D:\diplomna\DiplomnaStudentApi\DiplomnaStudentApi\DiplomnaStudentApi\WordSamples\ApplicationForSettlement.docx", tempFilePath, true);

                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
                Microsoft.Office.Interop.Word.Document aDoc = wordApp.Documents.Open(tempFilePath, ReadOnly: false, Visible: false);
                aDoc.Activate();

                ApplicationForSettlement applicationForSettlement = applicationForSettlementService.GetApplicationForSettlement(applicationSettlementId);
                StudentProfileDto studentProfile = studentProfileService.GetStudentProfile(applicationForSettlement.StudentId);


                FindAndReplace(wordApp, "${student}", $"{studentProfile.SecondName} {studentProfile.FirstName} {studentProfile.ThirdName}");
                FindAndReplace(wordApp, "${group}", studentProfile.Group);
                FindAndReplace(wordApp, "${faculty}", studentProfile.Faculty);
                FindAndReplace(wordApp, "${telephone}", studentProfile.Phone);
                FindAndReplace(wordApp, "${date}", applicationForSettlement.Date);
                FindAndReplace(wordApp, "${startYear}", applicationForSettlement.StartYear);
                FindAndReplace(wordApp, "${endYear}", applicationForSettlement.EndYear);
                if (studentProfile.SignImage != null)
                {
                    ReplaceImage(aDoc, "${studentSignature}", studentProfile.SignImage);
                }

                if (applicationForSettlement.DeanIdChecked)
                {
                    StudentProfileDto deanDto = studentProfileService.GetStudentProfile(applicationForSettlement.DeanId);
                    FindAndReplace(wordApp, "${dean}", $"{deanDto.SecondName} {deanDto.FirstName} {deanDto.ThirdName}");
                    if (deanDto.SignImage != null)
                    {
                        ReplaceImage(aDoc, "${deanSignature}", deanDto.SignImage);
                    }
                }

                if (applicationForSettlement.ComendantChecked)
                {
                    StudentProfileDto comendantDto = studentProfileService.GetStudentProfile(applicationForSettlement.ComendantId);
                    FindAndReplace(wordApp, "${comendant}", $"{comendantDto.SecondName} {comendantDto.FirstName} {comendantDto.ThirdName}");
                    if (comendantDto.SignImage != null)
                    {
                        ReplaceImage(aDoc, "${comendantSignature}", comendantDto.SignImage);
                    }
                }
                object missing = System.Reflection.Missing.Value;
                wordApp.Quit(true);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                return GetMemoryStreamWord(tempFilePath);
                

            }catch(Exception exc)
            {
                System.IO.File.Delete(tempFilePath);
                throw exc;
            }

        }
        
        private void ReplaceImage(Microsoft.Office.Interop.Word.Document aDoc, string key, byte[] image)
        {
            foreach (Microsoft.Office.Interop.Word.Shape s in aDoc.Shapes)
            {
                if (s.AlternativeText.Equals(key))
                {
                    WritePicture(s, image);
                }
            }
        }

        private MemoryStream GetMemoryStreamWord(string tempFilePath)
        {
            var stream = new FileStream(tempFilePath, FileMode.Open);
            var newStream = new MemoryStream();
            stream.CopyTo(newStream);
            newStream.Position = 0;
            stream.Close();
            System.IO.File.Delete(tempFilePath);
            return newStream;
        }

        private void WritePicture(Microsoft.Office.Interop.Word.Shape shape, byte [] image)
        {
            string tempFilePath = Path.GetTempFileName();
            FileStream fileStream = new FileStream(tempFilePath, FileMode.Open);
            fileStream.Write(image, 0, image.Length);
            shape.Fill.UserPicture(tempFilePath);
            fileStream.Close();
            File.Delete(tempFilePath);
        }

        private static void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}
