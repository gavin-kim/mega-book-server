using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mega_book.Controllers
{
    public class BookController : ApiController
    {
        private DataClasses1DataContext db; 
        public BookController()
        {
            db = new DataClasses1DataContext();
        }

        // GET: api/Book
        public IEnumerable<Book> Get()
        {
            List<Book> books = new List<Book>();
            books.AddRange(db.Books);
            return db.Books;
        }

        // GET: api/Book/5
        public object Get(int id)
        {
            // SingleOrDefault return Default(null) when there is no item.
            Book book = db.Books.Where(b => b.Id == id).SingleOrDefault();

            if (book == null)
                return NotFound();

            return book;
        }

        // POST: api/Book
        public int Post([FromBody]Book book)
        {
            if (db.Books.Any(v => v.Id == book.Id))
                return 0;

            db.Books.InsertOnSubmit(book);
            db.SubmitChanges();
            return book.Id;
        }

        // PUT: api/Book/5
        public void Put([FromBody]Book book)
        {
            Book original = db.Books.Where(b => b.Id == book.Id).SingleOrDefault();

            if (original == null || book.isbn == null || book.name == null ||
                book.releaseDate == null || book.content == null)
                return;

            original.isbn = book.isbn;
            original.name = book.name;
            original.releaseDate = book.releaseDate;
            original.content = book.content;

            db.SubmitChanges();
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
            Book book = db.Books.Where(b => b.Id == id).SingleOrDefault();

            if (book == null)
                return;

            db.Books.DeleteOnSubmit(book);
            db.SubmitChanges();
        }
    }
}
