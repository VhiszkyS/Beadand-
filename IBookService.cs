namespace Beadandó
{
    public interface IBookService
    {
        void Add (Book book);

        void Delete (Guid Id);

        Book Get (Guid Id);

        List<Book> GetAll ();

        void Update (Book newBook);
    }
}
