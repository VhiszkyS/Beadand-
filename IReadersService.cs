namespace Beadandó
{
    public interface IReaderService
    {
        void Add(Reader reader);

        void Delete(Guid Id);

        Reader Get(Guid Id);

        List<Reader> GetAll();

        void Update(Reader newReader);
    }
}
