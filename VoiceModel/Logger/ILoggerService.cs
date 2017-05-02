using System;
using VoiceModel.TropoModel;

namespace VoiceModel.Logger
{
    public interface ILoggerService
    {
        void Debug(string message);
        void Debug(string message, Result result);
        void Debug(string message, Session session);
        void Error(Exception ex);
        void Error(string message);
        void Fatal(Exception ex);
        void Fatal(string message);
        void Info(string message);
        void Warn(string message);
    }
}
