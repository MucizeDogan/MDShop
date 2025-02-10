using AutoMapper;
using MDShop.Comment.Context;
using MDShop.Comment.Dtos.CommentDtos;
using MDShop.Comment.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MDShop.Comment.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentsController : ControllerBase {
        private readonly CommentContext _context;
        private readonly IMapper _mapper;

        public CommentsController(CommentContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CommentList() {
            var comments = _context.UserComments.ToList();
            var commentDtos = _mapper.Map<List<ResultCommentDto>>(comments);
            return Ok(commentDtos);
        }

        [HttpPost]
        public IActionResult CreateComment(CreateCommentDto createCommentDto) {
            var comment = _mapper.Map<UserComment>(createCommentDto);
            _context.UserComments.Add(comment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla eklendi");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id) {
            var comment = _context.UserComments.Find(id);
            if (comment == null) {
                return NotFound("Yorum bulunamadı");
            }
            _context.UserComments.Remove(comment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id) {
            var comment = _context.UserComments.Find(id);
            if (comment == null) {
                return NotFound("Yorum bulunamadı");
            }
            var commentDto = _mapper.Map<GetByIdCommentDto>(comment);
            return Ok(commentDto);
        }

 
        [HttpPut]
        public IActionResult UpdateComment(UpdateCommentDto updateCommentDto) {
            var comment = _context.UserComments.Find(updateCommentDto.UserCommentId);
            if (comment == null) {
                return NotFound("Yorum bulunamadı");
            }

            _mapper.Map(updateCommentDto, comment);
            _context.SaveChanges();
            return Ok("Yorum başarıyla güncellendi");
        }


    }
}
