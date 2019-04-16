using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Inventory
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("InventoryReport", "Reports", "~/Reports/BowlsInventory.aspx",false, null, 
                new RouteValueDictionary(new { controller = new IncomingRequestConstraint() }));

            routes.MapPageRoute("PicturesReport", "Reports", "~/Reports/BowlsPictures.aspx", false, null,
                new RouteValueDictionary(new { controller = new IncomingRequestConstraint() }));


            routes.MapRoute(
                name: "Search",
                url: "{controller}/{action}/{sortOrder}/{currentFilter}/{search}/{page}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    sortOrder = UrlParameter.Optional,
                    currentFilter = UrlParameter.Optional,
                    search = UrlParameter.Optional,
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Edit", id = UrlParameter.Optional }
            );
        }

        public class IncomingRequestConstraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                return routeDirection == RouteDirection.IncomingRequest;
            }
        }
    }
}
