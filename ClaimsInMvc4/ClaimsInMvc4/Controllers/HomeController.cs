using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace ClaimsInMvc4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            SignInRequestMessage signInRequestMessage = new SignInRequestMessage(new Uri("https://andras1/idsrv/issue/wsfed"), "http://localhost:2533/");
            ViewBag.StsSignInUrl = signInRequestMessage.WriteQueryString();

            return View();
        }

        [ClaimsAuthorize("Show", "Code")]
        public ActionResult About()
        {
            if (ClaimsAuthorization.CheckAccess("Show", "Code"))
            {
                ViewBag.Message = "This is the secret code.";
            }
            else
            {
                ViewBag.Message = "Too bad.";
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
