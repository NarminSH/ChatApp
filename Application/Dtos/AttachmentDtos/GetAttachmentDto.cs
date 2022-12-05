using System;
using Application.Dtos.BaseDtos;
using Application.Dtos.PostDtos;

namespace Application.Dtos.AttachmentDtos
{
    public class GetAttachmentDto: BaseAuditableDto, IMapFrom<Attachment>
    {
        public int PostId { get; set; }
        public GetPostDto Post { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string? Type { get; set; }
    }
}

