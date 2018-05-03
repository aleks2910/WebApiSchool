using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApiSchool.Models;

namespace WebApiSchool.Controllers {

	[Authorize]
	public class EmptyController : ApiController {
		public string Get( int id ) {
			string requestInfo = "Контроллер: " + ControllerContext.ControllerDescriptor.ControllerName;
			requestInfo += " Url: " + ControllerContext.Request.RequestUri +
				" " + ControllerContext.Request.Method.Method;
			return requestInfo;
		}

		public HttpResponseMessage Get() {

			var response = Request.CreateResponse<IEnumerable<Book>>( HttpStatusCode.OK, new BookContext().Books );
			return response;

			//return new JsonResult<IEnumerable<Book>>( new BookContext().Books,  );
		}

	}
}
