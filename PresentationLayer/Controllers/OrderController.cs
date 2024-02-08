using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Messages;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text.RegularExpressions;

namespace PresentationLayer.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepositiry _orderRepositiry;
        private readonly IBookRepository _bookRepository;
        private readonly INotificationService _notificationService;
        private readonly IEnumerable<IDeliveryService> _deliveryService;

        public OrderController(IBookRepository bookRepository, IOrderRepositiry orderRepositiry, INotificationService notificationService, IEnumerable<IDeliveryService> deliveryService)
        {
            _bookRepository = bookRepository;
            _orderRepositiry = orderRepositiry;
            _notificationService = notificationService;
            _deliveryService = deliveryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = _orderRepositiry.GetById(cart.OrderId);
                OrderModelDTO model = Map(order);

                return View(model);
            }
            return View("Empty");
        }

        private OrderModelDTO Map(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);                      
            var books = _bookRepository.GetAllByIds(bookIds);
            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemModelDTO
                             {
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Price = item.Price,
                                 Count = item.Count
                             };
            return new OrderModelDTO
            {
                Id = order.Id,
                Items = itemModels.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice
            };
        }

        private (Order order, Cart cart) GetOrCreateOrderAndCart()                  //business logic
        {
            Order order;
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                order = _orderRepositiry.GetById(cart.OrderId);
            }
            else
            {
                order = _orderRepositiry.Create();
                cart = new Cart(order.Id);
            }

            return (order, cart);
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            _orderRepositiry.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);
        }

        [HttpPost]
        public IActionResult AddItem(int bookId, int count = 1)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            var book = _bookRepository.GetById(bookId);
            order.AddOrUpdateItem(book, count);
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Book", new {id = bookId });
        }

        [HttpPost]
        public IActionResult UpdateItem(int bookId, int count)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            order.GetItem(bookId).Count = count;
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult RemoveItem(int bookId)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            order.RemoveItem(bookId);
            SaveOrderAndCart(order, cart);

            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(int id, string cellPhone)
        {
            var order = _orderRepositiry.GetById(id);
            var model = Map(order);

            if (!IsValidCellPhone(cellPhone))
            {
                model.Errors["cellPhone"] = "incorrect number phone";
                return View("Index", model);
            }
            int code = 1111;   // todo: random.Next(1000, 10000) = 1000, 1001, ..., 9998, 9999
            HttpContext.Session.SetInt32(cellPhone, code);
            _notificationService.SendConfirmationCode(cellPhone, code);

            return View("Confirmation", new ConfirmationModel
                                        {
                                            OrderId = order.Id,
                                            CellPhone = cellPhone, 
                                        });
        }

        private bool IsValidCellPhone(string cellPhone)
        {
            if (cellPhone == null)
                return false;

            cellPhone = cellPhone.Replace(" ", "").Replace("-", "");

            return Regex.IsMatch(cellPhone, @"^\+?\d{11}$");
        }

        [HttpPost]
        public IActionResult Confirmate(int id, string cellPhone, int code)
        {
            int? storedCode = HttpContext.Session.GetInt32(cellPhone);
            if (storedCode == null)
            {
                return View("Confirmation", new ConfirmationModel
                {
                    OrderId = id,
                    CellPhone = cellPhone,
                    Errors = new Dictionary<string, string>
                                                {
                                                    { "code", "Time to enter code has expired. Try again." }
                                                },
                });
            }

            if (storedCode != code)
            {
                return View("Confirmation", new ConfirmationModel
                {
                    OrderId = id,
                    CellPhone = cellPhone,
                    Errors = new Dictionary<string, string>
                                                {
                                                    { "code", "Incorrect code. Check and try again." }
                                                },
                });
            }

            // todo: save CellPhone

            HttpContext.Session.Remove(cellPhone);
            var model = new DeliveryModel
            {
                OrderId = id,
                Methods = _deliveryService.ToDictionary(service => service.UniqueCode, service => service.Name)
            };
            return View("DeliveryMethod", model);
        }

        [HttpPost]
        public IActionResult StartDelivery (int id, string uniqueCode)
        {
            var deliveryService = _deliveryService.Single(service => service.UniqueCode == uniqueCode);
            var order = _orderRepositiry.GetById(id);
            var form = deliveryService.CreateForm(order);
            return View("DeliveryStep", form);
        }

        [HttpPost]
        public IActionResult NextDelivery (int id, string uniqueCode, int step, Dictionary<string, string> values) 
        {
            var deliveryService = _deliveryService.Single(service => service.UniqueCode == uniqueCode);
            var form = deliveryService.MoveNext(id, step, values);
            if (form.IsFinal)
            {
                return null;
            }
            return View("DeliveryStep", form);
        }
    }
}
