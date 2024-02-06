using DataAccessLayer.Interfaces;
using Domain;

namespace DataAccessLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 0201038013", "D. Knuth", "Art of Programming", "The bible of all fundamental algorithms and the work that taught many of today’s software developers most of what they know about computer programming.", 9.25m),
            new Book(2, "ISBN 0201485672", "M. Fowler", "Refactoring", "Any fool can write code that a computer can understand. Good programmers write code that humans can understand.", 5.99m),
            new Book(3, "ISBN 0131101633", "B. Kernighan, D. Ritchie", "C Programming Language", "he authors present the complete guide to ANSI standard C language programming. Written by the developers of C, this new version helps readers keep up with the finalized ANSI standard for C while showing how to take advantage of C's rich set of operators, economy of expression, improved control flow, and data structures.", 11.50m)
        };

        public Book[] GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                             join bookId in bookIds on book.Id equals bookId
                             select book;
            return foundBooks.ToArray();
        }

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn).ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Title.Contains(query) || book.Author.Contains(query)).ToArray();
        }

        public Book GetById(int id)
        {
            var book = books.Single(book => book.Id == id);
            return book;
        }


        //public List<Book> GetAllByIsbn(string isbn)
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        //var books = db.Books.Where(book => book.Isbn == isbn).ToList();
        //        var books = db.Books.Where(book => book.Isbn.Contains(isbn)).ToList();
        //        return books;
        //    }
        //}

        //public List<Book> GetAllByTitleOrAuthor(string textPartial)
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        var books = db.Books.Where(book => book.Title.Contains(textPartial) || book.Author.Name.Contains(textPartial)).ToList();
        //        return books;
        //    }
        //}

        //public Book GetById(int id)
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        var book = db.Books.Single(book => book.Id == id);
        //        return book;
        //    }
        //}
    }
}
