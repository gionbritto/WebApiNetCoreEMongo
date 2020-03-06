using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApiComMongoDB.Models;
using BooksApiComMongoDB.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace BooksApiComMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);
            if(book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if(book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if(book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }

        [HttpPost]
        public void Add()
        {
            InserirDados();
        }
        private void InserirDados()
        {
            Book book = new Book()
            {
                BookName = "Design Patterns",
                Price = 54.93M,
                Category = "Computers",
                Author = "Ralph Johnson"
            };
            _bookService.Create(book);
            //insertMany([{ 'Name':'Design Patterns', 'Price':54.93,'Category':'Computers', 'Author':'Ralph Johnson'}, { 'Name':'Clean Code','Price':43.15,'Category':'Computers','Author':'Robert C. Martin'}])
        }
    }
}