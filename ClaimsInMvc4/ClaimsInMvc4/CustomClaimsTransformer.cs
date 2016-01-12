using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Web;

namespace ClaimsInMvc4
{
    public class CustomClaimsTransformer : ClaimsAuthenticationManager
    {
        
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }

            ClaimsPrincipal transformedPrincipal = DressUpPrincipal(incomingPrincipal.Identity.Name);
            CreateSession(transformedPrincipal);

            return transformedPrincipal;
        }

        private void CreateSession(ClaimsPrincipal transformedPrincipal)
        {
            SessionSecurityToken sessionSecurityToken = new SessionSecurityToken(transformedPrincipal, TimeSpan.FromHours(8));
            sessionSecurityToken.IsPersistent = false;
            sessionSecurityToken.IsReferenceMode = true;
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionSecurityToken);
            FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenReceived += SessionAuthenticationModule_SessionSecurityTokenReceived;
            
        }

        void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            
        }

        void SessionAuthenticationModule_SessionSecurityTokenCreated(object sender, SessionSecurityTokenCreatedEventArgs e)
        {
            throw new NotImplementedException();
        }



        private ClaimsPrincipal DressUpPrincipal(String userName)
        {
            List<Claim> claims = new List<Claim>();

            //simulate database lookup
            if (userName.IndexOf("andras", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                claims.Add(new Claim(ClaimTypes.Country, "Sweden"));
                claims.Add(new Claim(ClaimTypes.GivenName, "Andras"));
                claims.Add(new Claim(ClaimTypes.Name, "Andras"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "Andras"));
                claims.Add(new Claim(ClaimTypes.Role, "IT"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.GivenName, userName));
                claims.Add(new Claim(ClaimTypes.Name, userName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
            }

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
        }
    }
}