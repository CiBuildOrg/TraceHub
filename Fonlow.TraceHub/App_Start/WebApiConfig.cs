﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;

namespace Fonlow.Web.Logging
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //http://forums.asp.net/t/1821729.aspx?JsonMediaTypeFormatter+does+not+work+with+Tuple+int+List+string+
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//   new DefaultContractResolver();
            //this will support Tuple serialization in JSON

            config.Services.RemoveAll(typeof(System.Web.Http.Validation.ModelValidatorProvider), (provider) => provider is System.Web.Http.Validation.Providers.InvalidModelValidatorProvider);
        }
    }
}
