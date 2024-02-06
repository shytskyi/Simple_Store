using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public IActionResult Index(int id)
        {
            Book book = _bookRepository.GetById(id);
            return View(book);
        }
    }
}
