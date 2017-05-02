using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoiceModel.Logger
{
    public class LoggerFactory
    {
        public static ILoggerService GetInstance()
        {
            return new Log4NetLoggerService();
        }

        public static IStartUpTask GetStartUpTask()
        {
            return new Log4NetStartUpTask();
        }
    }
}
