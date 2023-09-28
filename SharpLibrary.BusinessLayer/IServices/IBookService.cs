using SharpLibrary.Models.Entities;
using SharpLibrary.Shared.Dtos;

namespace SharpLibrary.BusinessLayer.IServices
{
    public interface IBookService
    {
        Task<Response<IEnumerable<Book>>> GetAllBooks();
        Task<Response<Book>> GetBookById(int id);
        Task<Response<bool>> AddBook(Book book);
        Task<Response<bool>> UpdateBook(Book book);
        Task<Response<bool>> DeleteBook(int id);
    }

}

