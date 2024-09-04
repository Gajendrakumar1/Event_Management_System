using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Event_Management_System
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Year", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Branch", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Student", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "EventMaster", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "LoginPage", action = "Login", id = UrlParameter.Optional }
                //defaults: new { controller = "EventLocationMaster", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Seat", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Event", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "College", action = "Index", id = UrlParameter.Optional }
                defaults: new { controller = "BookingInfo", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
