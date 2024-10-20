namespace Beadandó
{
    public interface IReaderService
    {
        void Add(Reader reader);

        void Delete(Guid id);

        Reader Get(Guid id);

        List<Reader> GetAll();

        void Update(Reader newReader);
    }
}
