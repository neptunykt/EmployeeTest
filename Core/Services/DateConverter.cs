using System;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public static class DateConverter
    {
       
        public static string ConvertDateToAcceptedFormat<T>(string inputDate, ILogger<T> logger = null)
        {
            try
            {
                var patternDDMMYYYY = new Regex(@"^(\d\d\/){2}\d{4}$");
                if (patternDDMMYYYY.Match(inputDate).Success)
                    return inputDate;

                var patternDMMYYYY = new Regex(@"^\d\/\d\d\/\d{4}$");
                if (patternDMMYYYY.Match(inputDate).Success)
                    return "0" + inputDate;

                var patternDMYYYY = new Regex(@"^(\d\/){2}\d{4}$");
                if (patternDMYYYY.Match(inputDate).Success)
                    return "0" + inputDate.Substring(0, 2) + "0" + inputDate.Substring(2);
                
                var patternDDMYYYY = new Regex(@"^\d\d\/\d\/\d{4}$");
                if (patternDDMYYYY.Match(inputDate).Success)
                    return inputDate.Substring(0, 3) + "0" + inputDate.Substring(3);


            }
            catch (Exception ex)
            {
                logger?.LogInformation(ex.Message);
            }

            return string.Empty;
        }
    }
}