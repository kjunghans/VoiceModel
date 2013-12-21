using System;
namespace VoiceModel.Logger
{
    public interface IStartUpTask
    {
        void Configure();
        bool IsEnabled { get; }
    }
}
