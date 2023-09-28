using AutoMapper;
using SharpLibrary.BusinessLayer.IServices;
using SharpLibrary.DataAccess.Interfaces;
using SharpLibrary.Models.Entities;
using SharpLibrary.Shared.Dtos;

namespace SharpLibrary.BusinessLayer.Services
{
    public class BookService : IBookService
	{
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<BookResponse>>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookResponses = _mapper.Map<IEnumerable<BookResponse>>(books);
            return Response<IEnumerable<BookResponse>>.Success(bookResponses, 200);
        }

        public async Task<Response<BookResponse>> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            var bookResponse = _mapper.Map<BookResponse>(book);
            return book != null
                ? Response<BookResponse>.Success(bookResponse, 200)
                : Response<BookResponse>.Fail("Book not found", 404);
        }

        public async Task<Response<bool>> AddBook(BookRequest bookRequest)
        {
            var book = _mapper.Map<Book>(bookRequest);
            await _bookRepository.AddAsync(book);
            return Response<bool>.Success(true, 201);
        }

        public async Task<Response<bool>> UpdateBook(UpdateBookRequest bookRequest)
        {
            var book = _mapper.Map<Book>(bookRequest);
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

