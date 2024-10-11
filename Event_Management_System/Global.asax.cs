using Event_Management_System.App_Start;
using Event_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Unity;
using Unity.Mvc5;
using MenuItem = Event_Management_System.Models.MenuItem;



namespace Event_Management_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Register the service with Dependency Injection
            DependencyResolver.SetResolver(new UnityDependencyResolver(CreateUnityContainer()));
        }
        private static IUnityContainer CreateUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<MenuItem>();
            return container;
        }
    }
}
