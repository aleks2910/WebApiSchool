using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiSchool.Models;

namespace WebApiSchool.Controllers {


	public class BookCustomController : ApiController {

		public IHttpActionResult Get( int id ) {

			Book book = new BookContext().Books.Find( id );
			return new HtmlContentResult( book );
		}
	}

	public class HtmlContentResult : IHttpActionResult {
		private Book model;

		public HtmlContentResult( Book model ) {
			this.model = model;
		}

		public Task<HttpResponseMessage> ExecuteAsync( CancellationToken cancellationToken ) {
			string bookInfo = "<html><head><meta charset=utf-8 /></head></body>" +
				"<h1>" + model.Name + "</h1><p>" + model.Author + "</p><p>"
				+ model.Year + "</p>" + "</body></html>";
			// объект ответа
			var response = new HttpResponseMessage();
			// создаем ответ
			response.Content = new StringContent( bookInfo );
			// отмечаем, что ответ будет в виде html
			response.Content.Headers.ContentType = new MediaTypeHeaderValue( "text/html" );

			return Task.FromResult( response );
		}
	}

}
