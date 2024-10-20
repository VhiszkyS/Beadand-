using System.Runtime.CompilerServices;

namespace Beadandó
{
    public class ReaderService : IReaderService
    {
        private List<Reader> _readerList;

        public void Add(Reader reader)
        {
            _readerList.Add(reader);
        }

        public void Delete(Guid id) 
        {
            _readerList.RemoveAll(p => p.Id == id);
        }

        public Reader Get(Guid id)
        {
            return _readerList.Find(p => p.Id == id);
        }

        public List<Reader> GetAll()
        {
            return _readerList;
        }

        public void Update(Reader newReader)
        {
            var existingReader = Get(newReader.Id);
            existingReader.Name = newReader.Name;
            existingReader.Address = newReader.Address;
            existingReader.BirthDate = newReader.BirthDate;
        }
    }
}
