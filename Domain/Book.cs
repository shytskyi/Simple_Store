namespace Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Isbn { get; set; }
        //public int AuthorId { get; set; }
        //public Author? Author { get; set; } 
        public string Author { get; set; }
        public Book(int id, string isbn, string aithor, string title, string description, decimal price)
        {
            Id = id;
            Isbn = isbn;
            Author = aithor;
            Title = title;
            Description = description;
            Price = price;
        }
    }
}
