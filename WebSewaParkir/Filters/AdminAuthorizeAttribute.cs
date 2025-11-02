using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSewaParkir.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Cek apakah session AdminLogin ada
            var admin = httpContext.Session["AdminLogin"];
            return admin != null; // true kalau sudah login
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Kalau belum login, arahkan ke halaman login
            filterContext.Result = new RedirectResult("~/Admin/Login");
        }
    }
}