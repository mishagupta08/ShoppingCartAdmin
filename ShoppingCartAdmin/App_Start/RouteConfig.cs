using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingCartAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AddAdminUser",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Dashboard", action = "AddAdminUser", id = UrlParameter.Optional }
        );

            routes.MapRoute(
          name: "AddImage",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Dashboard", action = "AddImage", id = UrlParameter.Optional }
      );

            routes.MapRoute(
       name: "AddProductImageUrls",
       url: "{controller}/{action}/{id}",
       defaults: new { controller = "Dashboard", action = "AddProductImageUrls", id = UrlParameter.Optional }
   );

            routes.MapRoute(
        name: "GetEditView",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Dashboard", action = "GetEditView", id = UrlParameter.Optional }
    );
        }
    }
}
