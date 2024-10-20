namespace Beadandó
{
    public interface IBookService
    {
        void Add (Book book);

        void Delete (Guid id);

        Book Get (Guid id);

        List<Book> GetAll ();

        void Update (Book newBook);
    }
}
