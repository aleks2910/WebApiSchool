using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebApiSchool.Filters;
using WebApiSchool.Models;

namespace WebApiSchool.Controllers {

	//[RoutePrefix("api")]
	// Use ~  to cancel prefix! - i.e. http://localhost:6392/lol/twit/2
	//[Route( "~/lol/twit/{id:int}" )]
	//public string Twit( int id ) {
	//	return id.ToString();
	//}
	public class ValuesController : ApiController {
		BookContext db = new BookContext();

		public IEnumerable<Book> GetBooks() {
			return db.Books;
		}

		// disable Authorization filter
		[OverrideAuthorization]
		public Book GetBook( int id ) {
			Book book = db.Books.Find( id );
			return book;
		}

		[Route( "api/values/getArray/{id}" )]
		[ArrayExceptionAttribute]
		public string GetArray( int id ) {
			string[] letters = new string[] { "aab", "aba", "baa" };
			return letters[id];
		}

		[Route( "api/values/getArray2/{id}" )]
		[CustomException( ExceptionType = typeof( IndexOutOfRangeException ),
		StatusCode = HttpStatusCode.BadRequest, Message = "Элемент вне диапазона" )]
		public string GetArray2( int id ) {
			string[] letters = new string[] { "aab", "aba", "baa" };
			return letters[id];
		}

		// used uniq route to avoid route conflict
		[Route( "api/values/authors" )]
		public IEnumerable<string> GetAuthors() {
			return db.Books.Select( b => b.Author ).Distinct();
		}

		// use template value in the custom route!
		[Route( "api/values/{id}/author" )]
		public string GetAuthor( int id ) {
			Book b = db.Books.Find( id );
			if( b != null )
				return b.Author;
			return "";
		}

		// default value and specified type
		[Route( "{id:int}/{name=volga}" )]
		public string Test( int id, string name ) {
			return id.ToString() + ". " + name;
		}

		[HttpGet]
		public int Sum( int num1, int num2 ) {
			return num1 + num2;
		}

		//[HttpPost]
		//public void CreateBook( [FromBody]Book book ) {
		//	db.Books.Add( book );
		//	db.SaveChanges();
		//}

		// POST api/values
		public IHttpActionResult Post( Book book ) {
			// отправка статусного кода 400
			if( book == null )
				return BadRequest();

			// обработка частных случаев валидации
			if( book.Year == 1984 )
				ModelState.AddModelError( "book.Year", "Год не должен быть равен 1984" );

			if( book.Name == "война и мир2" ) {
				ModelState.AddModelError( "book.Name", "Недопустимое название для книги" );
				ModelState.AddModelError( "book.Name", "Название не должно начинаться с мал. буквы" );
			}

			if( !ModelState.IsValid )
				return BadRequest( ModelState );

			db.Books.Add( book );
			db.SaveChanges();

			// если запрос без ошибок
			return Ok();
		}

		[HttpPut]
		public void EditBook( int id, [FromBody]Book book ) {
			if( id == book.Id ) {
				db.Entry( book ).State = EntityState.Modified;

				db.SaveChanges();
			}
		}

		public void DeleteBook( int id ) {
			Book book = db.Books.Find( id );
			if( book != null ) {
				db.Books.Remove( book );
				db.SaveChanges();
			}
		}
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				db.Dispose();
			}
			base.Dispose( disposing );
		}


		[Route( "api/values/setbooks" )]
		public void GetValue( [ModelBinder] Dictionary<string, Book> books ) {
			// обработка словаря
			var i = books.Keys.Count;
		}

		[Route( "api/values/setbooks" )]
		public void PostValue( [FromBody] Dictionary<string, Book> books ) {
			// обработка словаря
			var i = books.Keys.Count;
		}
	}

	#region old code
	//// GET api/values
	//public IEnumerable<string> Get() {
	//	return new string[] { "value1", "value2" };
	//}

	//// GET api/values/5
	//public string Get( int id ) {
	//	return "value";
	//}

	//// POST api/values
	//public void Post( [FromBody]string value ) {
	//}

	//// PUT api/values/5
	//public void Put( int id, [FromBody]string value ) {
	//}

	//// DELETE api/values/5
	//public void Delete( int id ) {
	//}
	#endregion
}
