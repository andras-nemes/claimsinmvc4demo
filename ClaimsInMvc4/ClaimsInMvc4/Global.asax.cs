using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClaimsInMvc4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            FederatedAuthentication.FederationConfigurationCreated += FederatedAuthentication_FederationConfigurationCreated;
            FederatedAuthentication.WSFederationAuthenticationModule.RedirectingToIdentityProvider += WSFederationAuthenticationModule_RedirectingToIdentityProvider;
        }

        void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
        {
            SignInRequestMessage signInRequestMessage = e.SignInRequestMessage;
        }

        void FederatedAuthentication_FederationConfigurationCreated(object sender, FederationConfigurationCreatedEventArgs e)
        {
            
        }

        /*
        protected void Application_PostAuthenticateRequest()
        {
            ClaimsPrincipal currentPrincipal = ClaimsPrincipal.Current;
            CustomClaimsTransformer customClaimsTransformer = new CustomClaimsTransformer();
            ClaimsPrincipal tranformedClaimsPrincipal = customClaimsTransformer.Authenticate(string.Empty, currentPrincipal);
            Thread.CurrentPrincipal = tranformedClaimsPrincipal;
            HttpContext.Current.User = tranformedClaimsPrincipal;
        }*/
    }
}