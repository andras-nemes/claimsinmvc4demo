using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ClaimsInMvc4
{
    public class CustomAuthorisationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				HttpRequest request = httpContext.Request;
				var queryParams = request.QueryString;
				foreach (var p in queryParams)
				{
					//do something
				}
			}

            string resource = context.Resource.First().Value;
            string action = context.Action.First().Value;

            if (action == "Show" && resource == "Code")
            {
                bool livesInSweden = context.Principal.HasClaim(ClaimTypes.Country, "Sweden");
                bool isAndras = context.Principal.HasClaim(ClaimTypes.GivenName, "Andras");
                //return isAndras && livesInSweden;
				return false;
            }

            return false;
        }
    }
}