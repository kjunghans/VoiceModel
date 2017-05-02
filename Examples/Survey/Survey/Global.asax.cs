using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Survey.Repository;
using VoiceModel;

namespace Survey
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            VoiceViewEngine.Register(ViewEngines.Engines);

            //Initialize database for test web application
            Database.SetInitializer<SurveyContext>(new DBInitializer());
            SurveyContext context = new SurveyContext();
            //Need to invoke context to have the DbInitializer do its stuff
            context.Surveys.Find(-1);

        }

        protected void Application_End()
        {
            SurveyContext context = new SurveyContext();
            context.Database.ExecuteSqlCommand("ALTER DATABASE VoiceSurvey SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            context.Database.Delete();

        }

    }
}