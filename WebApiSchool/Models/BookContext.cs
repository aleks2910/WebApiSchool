using System.Data.Entity;

namespace WebApiSchool.Models {
	public class BookContext : DbContext {
		public DbSet<Book> Books { get; set; }
	}
}