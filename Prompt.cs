using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace VoiceModel
{
    public class Prompt
    {
        public enum BargeinTypes  {speech, hotword, none};
        public BargeinTypes bargeintype { get; set; }
        public bool bargein { get; set; }
        public string language { get; set; }
        public int count { get; set; }
        public int timeout { get; set; }
        public List<IAudio> audios { get; set; }
        public Prompt()
        {
            this.count = 0;
            this.timeout = 0;
            this.bargeintype = BargeinTypes.none;
            this.bargein = true;
            this.language = string.Empty;
            this.audios = new List<IAudio>();
        }
        public Prompt(string textMsg)
        {
            this.count = 0;
            this.timeout = 0;
            this.bargeintype = BargeinTypes.none;
            this.bargein = true;
            this.language = string.Empty;
            this.audios = new List<IAudio>();
            this.audios.Add(new TtsMessage(textMsg));
        }
        public Prompt(IAudio audio)
        {
            this.count = 0;
            this.timeout = 0;
            this.bargeintype = BargeinTypes.none;
            this.bargein = true;
            this.language = string.Empty;
            this.audios = new List<IAudio>();
            this.audios.Add(audio);
        }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<prompt");
            if (!bargein)
                sb.Append(" bargein=\"false\"");
            if (bargeintype != BargeinTypes.none)
            {
                sb.Append(" barginetypes=\"");
                sb.Append(bargeintype.ToString());
                sb.Append("\"");
            }
            if (!language.Equals(string.Empty))
            {
                sb.Append(" xml:lang=\"");
                sb.Append(language);
                sb.Append("\"");
            }
            if (timeout > 0)
            {
                sb.Append(" timeout=\"");
                sb.Append(timeout.ToString());
                sb.Append("s\"");
            }
            if (count > 0)
            {
                sb.Append(" count=\"");
                sb.Append(count.ToString());
                sb.Append("\"");
            }
            sb.Append(">");
            sb.Append(Environment.NewLine);
            foreach (IAudio ia in audios)
            {
                sb.Append(ia.Render());
                sb.Append(Environment.NewLine);
            }
            sb.Append("</prompt>");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

    }
}