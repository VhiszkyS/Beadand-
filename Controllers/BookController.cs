using Microsoft.AspNetCore.Mvc;
using System;

namespace Beadandó.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController (IBookService bookService) 
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            var existingBook = _bookService.Get(book.Id);

            if (existingBook is not null)
            {
                return Conflict();
            }

            _bookService.Add(book);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var book = _bookService.Get(id);

            if (book is null)
            {
                return NotFound();
            }

            _bookService.Delete(id);

            return Ok();
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Book> Get(Guid id)
        {
            var book = _bookService.Get(id);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return Ok(_bookService.GetAll());
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Book newBook)
        {
            if (id != newBook.Id)
            {
                return BadRequest();
            }

            var existingBook = _bookService.Get(id);

            if (existingBook is null)
            {
                return NotFound();
            }

            _bookService.Update(newBook);

            return Ok();
        }
    }
}
