using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;
using System.Web.Script.Serialization;
using IronJS;
using IronJS.Hosting;

namespace GetDateDtmf 
{
    class ValidateDate 
    {

        class VoiceDate
        {
            public string Month { get; set; }
            public string Day { get; set; }
            public string Year { get; set; }
            public static VoiceDate Convert(string dtmfDate)
            {

                string[] months = {
                                      "January", "February", "March", "April", "May",
                                      "June", "July", "August", "September", "October",
                                      "November", "December"
                                  };
                VoiceDate vdate = new VoiceDate();
                int month = Int32.Parse(dtmfDate.Substring(0, 2));
                int day = Int32.Parse((dtmfDate.Substring(2, 2)));
                int year = Int32.Parse((dtmfDate.Substring(4, 2)));
                vdate.Day = day.ToString();
                vdate.Month = months[month - 1];
                int currYr = DateTime.Now.Year;
                int century = (currYr / 100) * 100;
                int nonCent = currYr % 100;
                int lowerBound;
                int upperBound;
                bool goDown = true;
                if ((nonCent - 25 ) < 0)
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
                if (year > lowerBound && year < upperBound)
                    year = year + century;
                else if (goDown)
                    year = year + (century - 100);
                else
                    year = year + (century + 100);
                vdate.Year = year.ToString();
                return vdate;
            }

            public  static string ConvertToJson(string dtmfDate)
            {
                VoiceDate vdate = Convert(dtmfDate);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(vdate);
            }
        }

        private CallFlow _cf;
        private State _state;

        public ValidateDate(CallFlow cf, State state)
        {
            _cf = cf;
            _state = state;
        }

        private  bool isValidDate(string date, out DateTime dateTime)
        {
            bool isValid = true;
            dateTime = DateTime.MinValue;
            try
            {
                int month = Int32.Parse(date.Substring(0, 2));
                int day = Int32.Parse((date.Substring(2, 2)));
                int year = Int32.Parse((date.Substring(4, 2)));
                if (month < 1 || month > 12)
                    isValid = false;
                if (day < 1 || day > 31)
                    isValid = false;
                dateTime = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                
                isValid = false;
            }

            return isValid;
        }

        public void Validate()
        {
            DateTime date;
           if (isValidDate(_state.jsonArgs, out  date))
           {
               GetDateDtmfOutput output = new GetDateDtmfOutput();
               output.Date = date;
               output.IsValidDate = true;
               _state.Ctx.SetGlobal<GetDateDtmfOutput>("GetDateDtmfOutput", output);
               string json = VoiceDate.ConvertToJson(_state.jsonArgs);
               _cf.FireEvent( "continue", json);
            }
            else
            {
                GetDateDtmfOutput output = new GetDateDtmfOutput();
                output.Date = date;
                output.IsValidDate = false;
                _state.Ctx.SetGlobal<GetDateDtmfOutput>("GetDateDtmfOutput", output);
                _cf.FireEvent("error", null);
           
            }
        }

    }
}
