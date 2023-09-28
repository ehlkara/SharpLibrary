using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpLibrary.BusinessLayer.IServices;
using SharpLibrary.Models.Entities;
using SharpLibrary.Shared.ControllerBases;

namespace SharpLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : CustomBaseController
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bookService.GetAllBooks();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _bookService.GetBookById(id);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookRequest book)
        {
            var response = await _bookService.AddBook(book);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBookRequest book)
        {
            var response = await _bookService.UpdateBook(book);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _bookService.DeleteBook(id);
            return CreateActionResultInstance(response);
        }
    }
}
