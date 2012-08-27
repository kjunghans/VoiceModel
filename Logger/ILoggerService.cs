using System;
namespace VoiceModel.Logger
{
    public interface ILoggerService
    {
        void Debug(string message);
        void Error(Exception ex);
        void Error(string message);
        void Fatal(Exception ex);
        void Fatal(string message);
        void Info(string message);
        void Warn(string message);
    }
}
