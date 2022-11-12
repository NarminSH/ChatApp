using System;
using Application.Dtos.ConnectionDtos;

namespace Application.Dtos.EmployeeDtos
{
    public class GetEmployee : IMapFrom<Employee>
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public ICollection<GetConnectionDto> GetConnectionDtos { get; set; }
    }
}

