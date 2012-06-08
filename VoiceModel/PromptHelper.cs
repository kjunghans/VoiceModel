using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace VoiceModel
{
    public static class PromptHelper
    {
        private static StringBuilder RenderPrompt(Prompt prompt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<prompt");
            if (!prompt.bargein)
                sb.Append(" bargein=\"false\"");
            if (prompt.bargeintype != Prompt.BargeinTypes.none)
            {
                sb.Append(" barginetypes=\"");
                sb.Append(prompt.bargeintype.ToString());
                sb.Append("\"");
            }
            if (!prompt.language.Equals(string.Empty))
            {
                sb.Append(" xml:lang=\"");
                sb.Append(prompt.language);
                sb.Append("\"");
            }
            if (prompt.timeout > 0)
            {
                sb.Append(" timeout=\"");
                sb.Append(prompt.timeout.ToString());
                sb.Append("s\"");
            }
            if (prompt.count > 0)
            {
                sb.Append(" count=\"");
                sb.Append(prompt.count.ToString());
                sb.Append("\"");
            }
            sb.Append(">");
            sb.Append(Environment.NewLine);
            foreach (IAudio ia in prompt.audios)
            {
                sb.Append(ia.Render());
                sb.Append(Environment.NewLine);
            }
            sb.Append("</prompt>");
            sb.Append(Environment.NewLine);
            return sb;

        }

        public static MvcHtmlString VoicePrompt(this HtmlHelper helper, Prompt prompt)
        {
            StringBuilder sb = RenderPrompt(prompt);
            return  new MvcHtmlString(sb.ToString());

        }

        private static StringBuilder RenderHandler(List<Prompt> prompts, string submitNext, string handlerType)
        {
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (Prompt prompt in prompts)
            {
                sb.Append("<" + handlerType + "count=\"");
                sb.Append(count.ToString());
                sb.Append("\">");
                sb.Append(Environment.NewLine);
                sb.Append(RenderPrompt(prompt));
                sb.Append("</" + handlerType +">");
                sb.Append(Environment.NewLine);
                count += 1;
            }
            sb.Append("<" + handlerType + "count=\"");
            sb.Append(count.ToString());
            sb.Append("\">");
            sb.Append(Environment.NewLine);
            sb.Append("<submit next=\"");
            sb.Append(submitNext);
            sb.Append("\" namelist=\"vm_id vm_event vm_result\" />");
            sb.Append(Environment.NewLine);
            sb.Append("</" + handlerType + ">");
            sb.Append(Environment.NewLine);
            return sb;
        }

        public static MvcHtmlString NoInput(this HtmlHelper helper, List<Prompt> prompts, string submitNext)
        {
            StringBuilder sb = RenderHandler(prompts, submitNext, "noinput");

            return new MvcHtmlString(sb.ToString());

        }

        public static MvcHtmlString NoMatch(this HtmlHelper helper, List<Prompt> prompts, string submitNext)
        {
            StringBuilder sb = RenderHandler(prompts, submitNext, "nomatch");

            return new MvcHtmlString(sb.ToString());

        }


    }
}
