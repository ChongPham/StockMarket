using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommnetDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CommentCreateDto commentCreateDto, int stockId)
        {
            return new Comment
            {
                Title = commentCreateDto.Title,
                Content = commentCreateDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this CommentUpdateDto commentUpdateDto)
        {
            return new Comment
            {
                Title = commentUpdateDto.Title,
                Content = commentUpdateDto.Content
            };
        }
    }
}