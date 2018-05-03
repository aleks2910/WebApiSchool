using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;
using WebApiSchool.Filters;
using WebApiSchool.Utils;

namespace WebApiSchool {
	public static class WebApiConfig {
		public static void Register( HttpConfiguration config ) {
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			// config.Filters.Add( new CustomAuthenticationAttribute() );

			config.Filters.Add( new CustomActionAttribute() );
			


			//add route to use actions directly
			config.Routes.MapHttpRoute(
				name: "BookRoute",
				routeTemplate: "api/{controller}/{action}",
				defaults: new { },
				constraints: new {
					action = new AlphaRouteConstraint(),
					// will be ignored 
					myConstraint = new CustomConstraint( "/api/values/get" ) 
				}
			);

			config.Routes.MapHttpRoute(
				name: "TwoParamRoute",
				routeTemplate: "api/{controller}/{action}/{num1}/{num2}"
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new {  },
				constraints: new {
					id = new IntRouteConstraint()
				}
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApiWithouParams",
				routeTemplate: "api/{controller}",
				defaults: new { id = RouteParameter.Optional }				
			);


			// добавление форматировщика
			config.Formatters.Add( new BookFormatter() );
		}
	}
}
