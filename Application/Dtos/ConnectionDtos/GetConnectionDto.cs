using System;
using Application.Dtos.BaseDtos;
using Application.Dtos.EmployeeDtos;

namespace Application.Dtos.ConnectionDtos
{
    public class GetConnectionDto : BaseAuditableDto, IMapFrom<Connection>
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public string SenderId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public ICollection<GetEmployeeDto>? Employees { get; set; }
        //todo complete dto
    }
}

