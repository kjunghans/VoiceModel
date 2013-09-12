using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using System.Web.Routing;
using System.IO;

namespace RecordingExample.Controllers
{
    public class RecordController : VoiceController
    {

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            //Make the first recording and playback
            flow.AddState(ViewStateBuilder.Build("getRecording", "playback",
                new Record("getRecording", "Please record your information after the beep.  Press the pound key when you are finished."))
                .AddOnExitAction(delegate(CallFlow cf, State state, Event e)
                {
                    VoiceModel.Logger.ILoggerService logger = VoiceModel.Logger.LoggerFactory.GetInstance();
                    try
                    {
                        //Copy last recording to another location
                        string recordingPhysicalPath = Server.MapPath(RecordingPath);
                        string recordingFileName = recordingPhysicalPath + "//" + cf.SessionId + ".wav";
                        logger.Debug("Copying recording " + recordingFileName);
                        System.IO.File.Copy(recordingFileName, "c://tmp//" + cf.SessionId + "_first.wav");
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error copying recording: " + ex.Message);
                    }
                }), true);
            Prompt playbackPrompt = new Prompt("You recorded ");
            playbackPrompt.audios.Add(new Audio(new ResourceLocation(new Var(flow, "recordingUri")),
                new Var(flow, "recordingName"), "Error finding recording"));
            State playbackState = ViewStateBuilder.Build("playback", "getRecording2", new Say("playback", playbackPrompt));
            playbackState.AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
            {
                cf["recordingUri"] = cf.RecordedAudioUri;
                cf["recordingName"] = cf.SessionId + ".wav";
            });
            flow.AddState(playbackState);

            //Make the second recording and playback
            flow.AddState(ViewStateBuilder.Build("getRecording2", "playback2",
                new Record("getRecording2", "Please record your information after the beep.  Press the pound key when you are finished."))
                .AddOnExitAction(delegate(CallFlow cf, State state, Event e)
                {
                    VoiceModel.Logger.ILoggerService logger = VoiceModel.Logger.LoggerFactory.GetInstance();
                    try
                    {
                        //Copy last recording to another location
                        string recordingPhysicalPath = Server.MapPath(RecordingPath);
                        string recordingFileName = recordingPhysicalPath + "//" + cf.SessionId + ".wav";
                        logger.Debug("Copying recording " + recordingFileName);
                        System.IO.File.Copy(recordingFileName, "c://tmp//" + cf.SessionId + "_second.wav");
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error copying recording: " + ex.Message);
                    }
                }));
            State playbackState2 = ViewStateBuilder.Build("playback2", "goodbye", new Say("playback2", playbackPrompt));
            playbackState.AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
            {
                cf["recordingUri"] = cf.RecordedAudioUri;
                cf["recordingName"] = cf.SessionId + ".wav";
            });
            flow.AddState(playbackState2);

            //Say goodbye and exit
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Your message has been saved. Goodbye.")));
            return flow;
           
        }

    }
}
