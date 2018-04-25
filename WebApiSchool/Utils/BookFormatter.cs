using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApiSchool.Models;

namespace WebApiSchool.Utils {
	class BookFormatter : MediaTypeFormatter {
		public BookFormatter() {
			SupportedMediaTypes.Add( new MediaTypeHeaderValue( "application/x-books" ) );
		}
		public override bool CanReadType( Type type ) {
			return type == typeof( Book ) || type == typeof( IEnumerable<Book> )
				|| type == typeof( IQueryable<Book> ) ;
		}
		public override bool CanWriteType( Type type ) {
			return type == typeof( Book ) || type == typeof( IEnumerable<Book> )
				|| type == typeof( IQueryable<Book> );
		}
		public override async Task WriteToStreamAsync( Type type, object value,
				Stream writeStream, HttpContent content, TransportContext transportContext ) {

			List<string> booksString = new List<string>();
			IEnumerable<Book> books = value is Book ? new Book[] { (Book)value } : (IEnumerable<Book>)value;
			foreach( Book b in books ) {
				booksString.Add( string.Format( "{0},{1},{2}", b.Id, b.Name, b.Year ) );
			}
			StreamWriter writer = new StreamWriter( writeStream );
			await writer.WriteAsync( string.Join( ",", booksString ) );
			writer.Flush();
		}
	}
}