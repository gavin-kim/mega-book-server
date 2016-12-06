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
        public Book Get(int id)
        {
            return db.Books.Where(book => book.Id == id).Single();
        }

        // POST: api/Book
        public void Post([FromBody]Book book)
        {
            System.Diagnostics.Debug.WriteLine(book.Id);
            System.Diagnostics.Debug.WriteLine(book.name);
            db.Books.InsertOnSubmit(book);
            db.SubmitChanges();
        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
            
        }
    }
}
