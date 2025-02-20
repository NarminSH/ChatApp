﻿using System;
using Application.Dtos.AttachmentDtos;
using Application.Dtos.BaseDtos;
using Application.Dtos.ConnectionDtos;
using Application.Dtos.EmployeeDtos;

namespace Application.Dtos.PostDtos
{
    public class GetPostDto:BaseAuditableDto, IMapFrom<Post>
    {
        public int ChannelId { get; set; }
        public GetConnectionDto? Channel { get; set; }
        public string EmployeeId { get; set; }
        public GetEmployeeDto? Employee { get; set; }
        public string Message { get; set; }
        public int? ReplyPostId { get; set; }
        public bool IsEdited { get; set; }
        public virtual GetPostDto? ReplyPost { get; set; }
        public virtual ICollection<GetPostDto>? Children { get; set; }
        //todo complete dto
        public ICollection<GetAttachmentDto>? Attachments { get; set; }
        //public ICollection<Reaction>? Reactions { get; set; }
    }
}

