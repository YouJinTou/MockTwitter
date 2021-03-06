﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Twitter.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tweets",
                url: "Tweets/{id}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Notifications",
                url: "Notifications/{id}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
