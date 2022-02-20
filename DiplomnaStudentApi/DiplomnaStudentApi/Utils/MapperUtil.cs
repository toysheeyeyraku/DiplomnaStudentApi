using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Utils
{
    public class MapperUtil
    {
        public static B CopyAToB<A, B>(A a)
        {
            B b = (B)Activator.CreateInstance(typeof(B));
            // copy fields
            var typeOfA = a.GetType();
            var typeOfB = b.GetType();
            foreach (var fieldOfA in typeOfA.GetFields())
            {
                var fieldOfB = typeOfB.GetField(fieldOfA.Name);
                fieldOfB.SetValue(b, fieldOfA.GetValue(a));
            }
            // copy properties
            foreach (var propertyOfA in typeOfA.GetProperties())
            {
                var propertyOfB = typeOfB.GetProperty(propertyOfA.Name);
                propertyOfB.SetValue(b, propertyOfA.GetValue(a));
            }

            return b;
        }
    
        public static int GetCurrentStudyYear()
        {
            int month = DateTime.Now.Month;
            if (month < 7)
            {
                return DateTime.Now.Year - 1;
            }
            else
            {
                return DateTime.Now.Year;
            }
        }
    
    }
}
