using BusinessLogicLayer;
using DataAccessLayer.Interfaces;
using Domain;
using Moq;

namespace SimpleStoreTests
{
    public class BookServiceTests
    {
        [Fact]
        public void Isbn_WithNull_ReturnFalse()
        {
            bool actual = BookService.IsIsbn(null);
            Assert.False(actual);
        }

        [Fact]
        public void Isbn_WithBlanSring_ReturnFalse()
        {
            bool actual = BookService.IsIsbn("   ");
            Assert.False(actual);
        }

        [Fact]
        public void Isbn_WithInvalidIsbn_ReturnFalse()
        {
            bool actual = BookService.IsIsbn("ISBN 123");
            Assert.False(actual);
        }

        [Fact]
        public void Isbn_WithIsbn10_ReturnTrue()
        {
            bool actual = BookService.IsIsbn("IsBn 123-456-789-0");
            Assert.True(actual);
        }

        [Fact]
        public void Isbn_WithIsbn13_ReturnTrue()
        {
            bool actual = BookService.IsIsbn("IsBn 123-456-789-0123");
            Assert.True(actual);
        }

        [Fact]
        public void Isbn_WithTrashStart_ReturnFalse()
        {
            bool actual = BookService.IsIsbn("xxIsBn 123-456-789-0123 xx");
            Assert.False(actual);
        }

        [Fact]
        public void GetAllByQuery_WhichIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                                       .Returns(new[] { new Book( 1, "", "", "", "", 0m) });

            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
                                      .Returns(new[] { new Book(2, "", "", "", "", 0m) });

            var validIsbn = "ISBN 12345-67890";
            var bookService = new BookService(bookRepositoryStub.Object);
            var actual1 = bookService.GetAllByQuery(validIsbn);
            Assert.Collection(actual1, book => Assert.Equal(1, book.Id));
        }

        [Fact]
        public void GetAllByQuery_WhichAuthor_GetAllByTitleOrAuthor()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();
            bookRepositoryStub.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                                       .Returns(new[] { new Book(1, "", "", "", "", 0m) });

            bookRepositoryStub.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
                                      .Returns(new[] { new Book(2, "", "", "", "", 0m) });

            var invalidIsbn = "12345-67890";
            var bookService = new BookService(bookRepositoryStub.Object);
            var actual1 = bookService.GetAllByQuery(invalidIsbn);
            Assert.Collection(actual1, book => Assert.Equal(2, book.Id));
        }
    }
}