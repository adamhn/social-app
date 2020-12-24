using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialApp.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Get the age from a date of birth datetime 
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns>Age Integer</returns>
        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
                DateTime.Today.Month == dateOfBirth.Month &&
                DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }

            return DateTime.Today.Year - dateOfBirth.Year;
        }
    }


}
