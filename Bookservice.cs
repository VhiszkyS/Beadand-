using System;

namespace Beadandó
{
    public class BookService : IBookService
    {
        private List<Book> _bookList;
        public void Add(Book book)
        {
            _bookList.Add(book);
        }
        public void Delete(Guid id)
        {
            _bookList.RemoveAll(p => p.Id == id);
        }
        public Book Get(Guid id)
        {
            return _bookList.Find(p => p.Id == id);
        }
        public List<Book> GetAll()
        {
           return _bookList;
        }
        public void Update(Book newBook)
        {
            var existingBook = Get(newBook.Id);
            existingBook.Title = newBook.Title;
            existingBook.Author = newBook.Author;
            existingBook.Publisher = newBook.Publisher;
            existingBook.PublishDate = newBook.PublishDate;
        }
    }
}
