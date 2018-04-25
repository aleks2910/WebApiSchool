using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebApiSchool.Utils {

	public class CustomConstraint : IHttpRouteConstraint {
		private string uri;
		public CustomConstraint( string uri ) {
			this.uri = uri;
		}
		public bool Match( HttpRequestMessage request, IHttpRoute route, string parameterName,
			IDictionary<string, object> values, HttpRouteDirection routeDirection ) {
			return !( uri == request.RequestUri.AbsolutePath );
		}
	}
}
