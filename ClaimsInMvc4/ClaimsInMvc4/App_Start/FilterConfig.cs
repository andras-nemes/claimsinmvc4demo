﻿using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Authorization.Mvc;

namespace ClaimsInMvc4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}