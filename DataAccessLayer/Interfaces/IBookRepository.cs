using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IBookRepository
    {
        //List<Book> GetAllByIsbn(string isbn);
        //List<Book> GetAllByTitleOrAuthor(string textPartial);
        Book GetById(int id);

        Book[] GetAllByIds(IEnumerable<int> bookIds);
        Book[] GetAllByIsbn(string isbn);
        Book[] GetAllByTitleOrAuthor(string textPartial);
    }
}
