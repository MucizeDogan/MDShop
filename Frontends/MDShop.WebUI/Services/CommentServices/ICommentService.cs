using MDShop.DtoLayer.CommentDtos;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.WebUI.Services.CommentServices {
    public interface ICommentService {
        Task<List<ResultCommentDto>> GetAllCommentAsync(bool isAdmin);
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
        Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task DeleteCommentAsync(string id);
        Task<UpdateCommentDto> GetByIdCommentAsync(string id);
        Task StatusChangeToTrue(string id);
        Task StatusChangeToFalse(string id);
        Task<List<ResultCommentDto>> CommentListByProductId(string id, [FromQuery] bool isAdmin = false);
    }
}
