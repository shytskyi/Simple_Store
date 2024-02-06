 namespace Domain
{
    public class OrderItem
    {
        private int count;
        public int Count
        {
            get
            {
                return count;
            } 
            set
            {
                ThrowIfInvalidCount(value);
                count = value;
            }
        }

        public int BookId { get; set; }

        public decimal Price { get; set; }


        public OrderItem(int bookid, int count, decimal price)
        {
            ThrowIfInvalidCount(count);
            BookId = bookid;
            Count = count;
            Price = price;
        }

        private static void ThrowIfInvalidCount(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");  
            }
        }
    }
}
