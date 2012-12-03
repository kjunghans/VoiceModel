using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoiceModel;
using VoiceModel.CallFlow;
using System.Web.Routing;

namespace RecordingExample.Controllers
{
    public class RecordController : VoiceController
    {

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            flow.AddState(ViewStateBuilder.Build("getRecording", "playback",
                new Record("getRecording", "Please record your information after the beep.  Press the pound key when you are finished.")),true);
            Prompt playbackPrompt = new Prompt("You recorded ");
            playbackPrompt.audios.Add(new Audio(new ResourceLocation(new Var(flow, "recordingUri")),
                new Var(flow, "recordingName"), "Error finding recording"));
            State playbackState = ViewStateBuilder.Build("playback", "goodbye", new Say("playback", playbackPrompt));
            playbackState.AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
            {
                cf["recordingUri"] = cf.RecordedAudioUri;
                cf["recordingName"] = cf.SessionId + ".wav";
            });
            flow.AddState(playbackState);
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Your message has been saved. Goodbye.")));
            return flow;
           
        }

    }
}
