using Microsoft.AspNetCore.Mvc;

namespace Beadandó.Controllers
{
    [ApiController]
    [Route("readers")]
    public class ReaderController : ControllerBase
    {
        private IReaderService _readerService;

        public ReaderController(IReaderService readerService) 
        {
            _readerService = readerService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Reader reader) 
        {
            var existingReader = _readerService.Get(reader.Id);

            if (existingReader != null) 
            {
                return Conflict();
            }

            _readerService.Add(reader);

            return Ok();

        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id) 
        {
            var reader = _readerService.Get(id);

            if (reader == null) 
            {
                return NotFound();
            }

            _readerService.Delete(id);
            
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id) 
        {
            var reader = _readerService.Get(id);

            if (reader == null) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Reader>> Get()
        {
            return Ok(_readerService.GetAll());
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Reader newReader)
        {
            if (id != newReader.Id)
            {
                return BadRequest();
            }

            var existingReader = _readerService.Get(id);

            if (existingReader is null)
            {
                return NotFound();
            }

            _readerService.Update(newReader);

            return Ok();
        }
    }

    
}
