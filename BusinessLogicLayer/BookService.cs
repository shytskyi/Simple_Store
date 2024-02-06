using DataAccessLayer.Interfaces;
using Domain;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
       
        public Book[] GetAllByQuery(string query)
        {
            if (IsIsbn(query))
                    return _bookRepository.GetAllByIsbn(query);

            return _bookRepository.GetAllByTitleOrAuthor(query);
        }

        internal static bool IsIsbn(string query)
        {
            if (query == null)
                return false;
            query = query.Replace("-", "").Replace(" ", "").ToUpper();
            return Regex.IsMatch(query, @"^ISBN\d{10}(\d{3})?$");
        }


        //public List<Book> GetAllByQuery(string query)
        //{
        //    if (IsIsbn(query))
        //        return _bookRepository.GetAllByIsbn(query);

        //    return _bookRepository.GetAllByTitleOrAuthor(query);
        //}

        //private bool IsName (string query)
        //{
        //    if (query == null)
        //        return false;
        //    return Regex.IsMatch(query, @"^[a-zA-Z]+(\s[a-zA-Z]+)?$");
        //}
    }
}