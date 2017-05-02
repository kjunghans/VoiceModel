using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetDateDtmf;
using VoiceModel;
using VoiceModel.CallFlow;
using IronJS;
using IronJS.Hosting;
using System.Web.Script.Serialization;

namespace ReusableComponentEx.Controllers
{
    public class MainController : VoiceController
    {

        public override CallFlow BuildCallFlow()
        {
            CallFlow flow = new CallFlow();
            List<VxmlProperty> appProperties = new List<VxmlProperty>();
            appProperties.Add( new VxmlProperty("inputmode", "dtmf"));
            flow.AddState(ViewStateBuilder.Build("greeting", "getStartDate",
                new Say("greeting", new Prompt("Welcome to the Reusable Dialog Component Example.") {bargein = false }) 
                { properties = appProperties }), true);

            //This tells the state machine to use the state machine in the reusable component.
            flow.AddState(new State("getStartDate", "getFinishDate")
                .AddNestedCallFlow(new GetDateDtmfRDC(new Prompt("Enter the start date as a six digit number.")))
                .AddOnExitAction(delegate(CallFlow cf, State state, Event e)
                {cf.Ctx.SetGlobal<GetDateDtmfOutput>("StartDate",((GetDateDtmfRDC)state.NestedCF).GetResults());}));
            
            //We will reuse the component again for the finish date.
            flow.AddState(new State("getFinishDate", "calcDifference")
                .AddNestedCallFlow(new GetDateDtmfRDC(new Prompt("Enter the finish date as a six digit number.")))
                .AddOnExitAction(delegate(CallFlow cf, State state, Event e)
                {cf.Ctx.SetGlobal<GetDateDtmfOutput>("FinishDate",((GetDateDtmfRDC)state.NestedCF).GetResults());}));

            //Calculate the difference in days
            flow.AddState(new State("calcDifference","differenceInDays")
                .AddOnEntryAction(delegate(CallFlow cf, State state, Event e)
                {
                    var startDate = cf.Ctx.GetGlobalAs<GetDateDtmfOutput>("StartDate");
                    var finishDate = cf.Ctx.GetGlobalAs<GetDateDtmfOutput>("FinishDate");
                    string daysDiff = finishDate.Date.Subtract(startDate.Date).Days.ToString();
                    var d = new { daysDiff = daysDiff };
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(d);
                    cf.FireEvent("continue", json);
                }));

            Prompt sayDiff = new Prompt();
            sayDiff.audios.Add(new TtsMessage("The difference between the start and finish dates is "));
            sayDiff.audios.Add(new TtsVariable("d.daysDiff"));
            sayDiff.audios.Add(new TtsMessage(" days."));
            sayDiff.bargein = false;
            flow.AddState(ViewStateBuilder.Build("differenceInDays", "goodbye", new Say("differenceInDays", sayDiff)));
            flow.AddState(ViewStateBuilder.Build("goodbye", new Exit("goodbye", "Goodbye.")));
            return flow;

        }

    }
}
