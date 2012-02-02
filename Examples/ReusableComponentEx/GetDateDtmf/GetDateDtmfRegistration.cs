using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace GetDateDtmf
{
    class GetDateDtmfRegistration : PortableAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GetDateDtmf";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute("resources",
                             AreaName + "/Resource/{resourceName}",
                             new {Controller = "EmbeddedResource", action = "Index"},
                             new string[] {"MvcContrib.PortableAreas"});

            base.RegisterArea(context, bus);

            context.MapRoute(
                "GetDateDtmf",
                AreaName + "/{controller}/{action}/{id}",
                new { controller = "GetDateDtmf", action = "Index", id = UrlParameter.Optional }
                );
        }

    }
}
