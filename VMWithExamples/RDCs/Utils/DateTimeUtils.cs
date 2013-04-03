using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RDCs.Utils
{
    public class VoiceDate
    {
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
    }

    public class DateTimeUtils
    {
        public static int TwoDigitYearToFourDigit(int twoDigitYear)
        {
            int currYr = DateTime.Now.Year;
            int century = (currYr / 100) * 100;
            int nonCent = currYr % 100;
            int lowerBound;
            int upperBound;
            bool goDown = true;
            if ((nonCent - 25) < 0)
                lowerBound = 0;
            else
                lowerBound = nonCent - 25;
            if ((nonCent + 25) > 99)
            {
                upperBound = 99;
                goDown = false;
            }
            else
                upperBound = nonCent + 25;
            int fourDigitYear = 0;
            if (twoDigitYear > lowerBound && twoDigitYear < upperBound)
                fourDigitYear = twoDigitYear + century;
            else if (goDown)
                fourDigitYear = twoDigitYear + (century - 100);
            else
                fourDigitYear = twoDigitYear + (century + 100);

            return fourDigitYear;
        }

        public static VoiceDate Convert(string dtmfDate)
        {
            string[] months = {
                                      "January", "February", "March", "April", "May",
                                      "June", "July", "August", "September", "October",
                                      "November", "December"
                                  };
            VoiceDate vdate = new VoiceDate();
            if (dtmfDate.Length != 6)
                throw new Exception("Date must be six digits");
            string strMonth = dtmfDate.Substring(0, 2);
            int month = 0;
            try
            {
                month = Int32.Parse(strMonth);
            }
            catch { throw new Exception("Cannot parse [" + strMonth + "] to a valid month."); }
            if (month < 1 || month > 12)
                throw new Exception("The month is not in a valid range.");
            int day = 0;
            string strDay = dtmfDate.Substring(2, 2);
            try
            {
                day = Int32.Parse(strDay);
            }
            catch { throw new Exception("Cannot parse [" + strDay + "] to a valid day."); }
            if (day < 1 || day > 31)
                throw new Exception("The day is not in a valid range.");
            int year = 0;
            string strYear = dtmfDate.Substring(4, 2);
            try
            {
                year = Int32.Parse(strYear);
            }
            catch { throw new Exception("Cannot parse [" + strYear + "] to a valid year."); }
            vdate.Day = day.ToString();
            vdate.Month = months[month - 1];
            vdate.Year = TwoDigitYearToFourDigit(year).ToString();
            return vdate;
        }

        public static string ConvertToJson(string dtmfDate)
        {
            VoiceDate vdate = Convert(dtmfDate);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(vdate);
        }

    }
}
