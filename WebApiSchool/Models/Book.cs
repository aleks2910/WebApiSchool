using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiSchool.Models {
	public class Book {
		public int Id { get; set; }

		[Required( ErrorMessage = "Укажите название книги" )]
		public string Name { get; set; }

		[Range( 1800, 2000, ErrorMessage = "Год должен быть в промежутке от 1800 до 2000" )]
		[Required( ErrorMessage = "Укажите год издания книги" )]
		public int Year { get; set; }

		public string Author { get; set; }
	
	}
}