namespace Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalCount => _items.Sum(item => item.Count);
        public decimal TotalPrice => _items.Sum(item => item.Price * item.Count) + (Delivery?.AmountDelivery ?? 0m);
        public string CellPhone { get; set; }
        public OrderDelivery Delivery { get; set; } 
        public OrderPayment Payment { get; set; }

        private List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items // потім переробити щоб лишився тільки "List<OrderItem>" 
        {
            get { return _items; } 
        }

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            Id = id;
            _items = new List<OrderItem>(items);
        }



        public OrderItem GetItem(int bookId)                                                        //business logic
        {
            int index = _items.FindIndex(item => item.BookId == bookId);
            if (index == -1)
                ThrowBookException("Book not found.", bookId);

            return _items[index];
        }

        public void AddOrUpdateItem(Book book, int count) 
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            int index = _items.FindIndex(item => item.BookId == book.Id);
            if (index == -1)
                _items.Add(new OrderItem(book.Id, count, book.Price));
            else
                _items[index].Count += count;
        }

        public void RemoveItem ( int bookId)
        {
            int index = _items.FindIndex(item => item.BookId == bookId);
            if(index == -1)
                ThrowBookException("Order does not contain specified item.", bookId);

            _items.RemoveAt(index);
        }

        private void ThrowBookException (string massage, int bookId)                    //if we want to pass additional parameters to exception
        {
            var exception = new InvalidOperationException(massage);

            exception.Data["BookId"] = bookId;


            throw exception;
        }
    }
}
