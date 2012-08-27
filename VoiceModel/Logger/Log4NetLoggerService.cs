﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace VoiceModel.Logger
{
    public class Log4NetLoggerService : ILoggerService
    {
        private ILog _logger;

        public Log4NetLoggerService()
        {
            _logger = LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }
        public void Warn(string message)
        {
            _logger.Warn(message);
        }
        public void Debug(string message)
        {
            _logger.Debug(message);
        }
        public void Error(string message)
        {
            _logger.Error(message);
        }
        public void Error(Exception ex)
        {
            _logger.Error(ex.Message, ex);
        }
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }
        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex.Message, ex);
        }
    }
}
