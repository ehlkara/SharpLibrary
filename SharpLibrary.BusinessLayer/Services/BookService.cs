using SharpLibrary.BusinessLayer.IServices;
using SharpLibrary.DataAccess.Interfaces;
using SharpLibrary.Models.Entities;
using SharpLibrary.Shared.Dtos;

namespace SharpLibrary.BusinessLayer.Services
{
    public class BookService : IBookService
	{
        private readonly IGenericRepository<Book> _bookRepository;

        public BookService(IGenericRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Response<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Response<IEnumerable<Book>>.Success(books, 200);
        }

        public async Task<Response<Book>> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book != null
                ? Response<Book>.Success(book, 200)
                : Response<Book>.Fail("Book not found", 404);
        }

        public async Task<Response<bool>> AddBook(Book book)
        {
            await _bookRepository.AddAsync(book);
            return Response<bool>.Success(true, 201);
        }

        public async Task<Response<bool>> UpdateBook(Book book)
        {
            await _bookRepository.UpdateAsync(book);
            return Response<bool>.Success(true, 200);
        }

        public async Task<Response<bool>> DeleteBook(int id)
        {
            await _bookRepository.DeleteAsync(id);
            return Response<bool>.Success(true, 200);
        }
    }
}

