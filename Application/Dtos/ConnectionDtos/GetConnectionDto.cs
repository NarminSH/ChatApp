using System;
using Application.Dtos.BaseDtos;
using Application.Dtos.EmployeeDtos;
using Application.Dtos.PostDtos;

namespace Application.Dtos.ConnectionDtos
{
    public class GetConnectionDto : BaseAuditableDto, IMapFrom<Connection>
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        //public ICollection<GetEmployeeChannelDto>? EmployeeChannels { get; set; }
        public ICollection<GetPostDto>? Posts { get; set; }
    }
}

