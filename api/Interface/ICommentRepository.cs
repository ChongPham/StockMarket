using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment commentModel);
        Task<Comment?> UpdateCommentAysnc(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}