using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace WebApiSchool.Controllers {
	public class ExchangeController : IHttpController {
		public Task<HttpResponseMessage> ExecuteAsync( HttpControllerContext context,
													CancellationToken cancellationToken ) {
			return Task<HttpResponseMessage>.Run( () => {
				IHttpRouteData rd = context.RouteData;
				string output = "Неверный запрос";
				if( rd.Values.ContainsKey( "id" ) ) {
					int sum;
					if( int.TryParse( (string)rd.Values["id"], out sum ) && sum > 0 ) {
						double result = 1.3 * sum;
						output = String.Format( "За {0} евро вы получите {1} долларов", sum, result );
					}
				}
				return context.Request.CreateResponse( HttpStatusCode.OK, output );
			} );
		}


	}
}