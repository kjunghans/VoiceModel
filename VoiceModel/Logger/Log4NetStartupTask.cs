using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VoiceModel.Logger
{
    public class Log4NetStartUpTask : IStartUpTask
    {
        public bool IsEnabled { get { return true; } }

        public void Configure()
        {
            log4net.Config.XmlConfigurator.Configure(
                new System.IO.FileInfo(
                    HttpContext.Current.Server.MapPath("~/log4net.config")));
        }
    }
}
