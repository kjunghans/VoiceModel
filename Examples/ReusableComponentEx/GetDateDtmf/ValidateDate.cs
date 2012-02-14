using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceModel.CallFlow;
using System.Web.Script.Serialization;

namespace GetDateDtmf 
{
    class ValidateDate : State
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

        public override void OnEntry()
        {
            DateTime date;
           if (isValidDate(this.jsonArgs, out  date))
           {
               GetDateDtmfOutput output = new GetDateDtmfOutput();
               output.Date = date;
               output.IsValidDate = true;
               Flows.SessionMgr.SetComponentOutput(output);
               string json = VoiceDate.ConvertToJson(this.jsonArgs);
               this.Flows.FireEvent(this.Id, "continue", json);
            }
            else
            {
                GetDateDtmfOutput output = new GetDateDtmfOutput();
                output.Date = date;
                output.IsValidDate = false;
                Flows.SessionMgr.SetComponentOutput(output);
                this.Flows.FireEvent(this.Id, "error", null);
           
            }
        }

        public ValidateDate(string id, string successTarget, string invalidTarget)
            : base(id)
        {
            this.AddTransition("continue", successTarget, null);
            this.AddTransition("error", invalidTarget, null);
        }
    }
}
