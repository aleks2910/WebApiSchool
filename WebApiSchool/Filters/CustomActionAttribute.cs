using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiSchool.Filters {

	public class CustomActionAttribute : Attribute, IActionFilter {

		public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
							HttpActionContext actionContext, CancellationToken cancellationToken,
							Func<Task<HttpResponseMessage>> continuation ) {

			Stopwatch timer = Stopwatch.StartNew();
			HttpResponseMessage result = await continuation();
			double seconds = timer.ElapsedMilliseconds / 1000.0;
			result.Headers.Add( "Elapsed-Time", seconds.ToString() );
			return result;
		}
		public bool AllowMultiple {
			get { return false; }
		}
	}


	// other example
	public class CustomActionAttribute2 : ActionFilterAttribute {
		private DateTime start;

		public override Task OnActionExecutingAsync( HttpActionContext actionContext,
			CancellationToken cancellationToken ) {
			return Task.Run( () => { start = DateTime.Now; } );
		}

		public override Task OnActionExecutedAsync( HttpActionExecutedContext actionExecutedContext,
										CancellationToken cancellationToken ) {
			return Task.Run( () => {
				DateTime end = DateTime.Now;
				actionExecutedContext.Response.Headers.Add( "Start-Time", start.ToLongTimeString() );
				actionExecutedContext.Response.Headers.Add( "End-Time", end.ToLongTimeString() );
			} );
		}
	}

}
