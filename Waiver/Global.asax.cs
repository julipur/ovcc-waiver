using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using Waiver.Business;

namespace Waiver
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



            EZFontResolver fontResolver = EZFontResolver.Get;
            // Assign it to PDFsharp.
            GlobalFontSettings.FontResolver = fontResolver;

            // We only have Janitor Regular, no Bold, no Italic.
            // We allow PDFsharp to simulate Bold and Italic for us.
            fontResolver.AddFont("Arial", XFontStyle.Regular, Server.MapPath("/fonts/arial.ttf"), true, true);
        }
    }
}
