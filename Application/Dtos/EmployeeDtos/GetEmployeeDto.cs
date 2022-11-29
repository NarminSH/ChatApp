using System;
using Application.Dtos.BaseDtos;
using Application.Dtos.ConnectionDtos;

namespace Application.Dtos.EmployeeDtos
{
    public class GetEmployeeDto : IMapFrom<Employee>
    {
        public string Id { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public ICollection<GetConnectionDto>? GetConnectionDtos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

