using api.DTOs.Comment;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockContext;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockContext)
        {
            _commentRepo = commentRepo;
            _stockContext = stockContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComment()
         {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(x => x.ToCommnetDto());
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommnetDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CommentCreateDto commentCreateDto)
        {
            var stockExist = await _stockContext.StockExist(stockId);
            if (stockExist == false)
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = commentCreateDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommnetDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CommentUpdateDto commentUpdateDto)
        {
            var comment = await _commentRepo.UpdateCommentAysnc(id, commentUpdateDto.ToCommentFromUpdate());
            if (comment == null)
            {
                return NotFound("Comment does not exist!");
            }

            return Ok(comment.ToCommnetDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("No comment to delete!");
            }

            return NoContent();
        }
    }
}