using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WebApiSchool.Models;

namespace WebApiSchool.Controllers {
	public class ValuesController : ApiController {
		BookContext db = new BookContext();

		public IEnumerable<Book> GetBooks() {
			return db.Books;
		}

		public Book GetBook( int id ) {
			Book book = db.Books.Find( id );
			return book;
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
