using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mega_book.Controllers
{
    // Be very careful about setting SupportsCredentials to true, because it means a website at another domain 
    // can send a logged-in user’s credentials to your Web API on the user’s behalf, without the user being aware. 
    // The CORS spec also states that setting origins to "*" is invalid if SupportsCredentials is true.

    // allows cross-origin requests from web client (!! any website make ajax calls to the web API)
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReviewController : ApiController
    {
        private DataClasses1DataContext db;

        public ReviewController()
        {
            db = new DataClasses1DataContext();
        }


        // GET: api/Review
        public IEnumerable<Review> Get([FromUri] ReviewQuery query)
        {
            System.Diagnostics.Debug.WriteLine("Get");
            System.Diagnostics.Debug.WriteLine(query);

            // Queryable stores query
            IQueryable<Review> queryable = db.Reviews.AsQueryable();

            // update queryable for options
            if (query.BookId != 0)
                queryable = queryable.Where(review => review.bookId == query.BookId).AsQueryable();

            if (query.BookName != null)
                queryable = queryable.Where(review => review.Book.name.Equals(query.BookName)).AsQueryable();

            if (query.ReviewerName != null)
                queryable = queryable.Where(review => review.reviewerName.Equals(query.ReviewerName)).AsQueryable();

            List<Review> reviews = queryable.ToList();
            return reviews;
        }

        // GET: api/Review/query
        public object GetById(int id)
        {
            System.Diagnostics.Debug.WriteLine("GetById");

            Review review = db.Reviews.Where(v => v.Id == id).SingleOrDefault();

            if (review == null)
                return NotFound();

            return review;
        }

        // POST: api/Review
        public void Post([FromBody]Review review)
        {
            db.Reviews.InsertOnSubmit(review);
            db.SubmitChanges();
        }

        // PUT: api/Review/5
        public void Put(int id, [FromBody]string value)
        {
          
        }

        // DELETE: api/Review/5
        public void Delete(int id)
        {
        }
    }
    public class ReviewQuery
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ReviewerName { get; set; }

        public override string ToString()
        {
            return $"BookId: {BookId}, BookName: {BookName}, ReviewerName: {ReviewerName}";
        }
    }
}
