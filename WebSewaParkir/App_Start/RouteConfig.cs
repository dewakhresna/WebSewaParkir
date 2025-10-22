using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSewaParkir
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DashboardAdmin",
                url: "admin/dashboard",
                defaults: new { controller = "Admin", action = "Dashboard" }
            );

            routes.MapRoute(
                name: "LoginAdmin",
                url: "admin/login",
                defaults: new { controller = "Admin", action = "Login" }
            );

            routes.MapRoute(
                name: "User",
                url: "admin/user",
                defaults: new { controller = "Admin", action = "User" }
            );

            routes.MapRoute(
                name: "AddUser",
                url: "admin/add_user",
                defaults: new { controller = "Admin", action = "AddUser" }
            );

            routes.MapRoute(
                name: "EditUser",
                url: "admin/edit_user",
                defaults: new { controller = "Admin", action = "EditUser" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "admin/profile",
                defaults: new { controller = "Admin", action = "Profile" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
