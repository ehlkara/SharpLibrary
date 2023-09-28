using SharpLibrary.Models.Entities;
using SharpLibrary.Shared.Dtos;

namespace SharpLibrary.BusinessLayer.IServices
{
    public interface IBookService
    {
        Task<Response<IEnumerable<BookResponse>>> GetAllBooks();
        Task<Response<BookResponse>> GetBookById(int id);
        Task<Response<bool>> AddBook(BookRequest bookRequest);
        Task<Response<bool>> UpdateBook(UpdateBookRequest bookRequest);
        Task<Response<bool>> DeleteBook(int id);
    }

}

