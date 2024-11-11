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
        public async Task <IActionResult> Add([FromBody] Reader reader) 
        {
            var existingReader = await _readerService.GetAsync(reader.Id);

            if (existingReader != null) 
            {
                return Conflict();
            }

            await _readerService.AddAsync(reader);

            return Ok();

        }

        [HttpDelete("{id:guid}")]
        public async Task <IActionResult> Delete(Guid id) 
        {
            var reader = await _readerService.GetAsync(id);

            if (reader == null) 
            {
                return NotFound();
            }

           await _readerService.DeleteAsync(id);
            
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task <IActionResult> Get(Guid id) 
        {
            var reader = await _readerService.GetAsync(id);

            if (reader == null) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        public async Task <ActionResult<List<Reader>>> Get()
        {
            return Ok(_readerService.GetAllAsync());
        }

        [HttpPut("{id:guid}")]
        public async Task <IActionResult> Update(Guid id, [FromBody] Reader newReader)
        {
            if (id != newReader.Id)
            {
                return BadRequest();
            }

            var existingReader = await _readerService.GetAsync(id);

            if (existingReader is null)
            {
                return NotFound();
            }

            await _readerService.UpdateAsync(newReader);

            return Ok();
        }
    }

    
}
