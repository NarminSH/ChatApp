﻿using System;
using Application.Dtos.BaseDtos;
using Application.Dtos.ConnectionDtos;

namespace Application.Dtos.EmployeeDtos
{
    public class GetEmployeeChannelDto: BaseAuditableDto, IMapFrom<EmployeeChannel>
    {
        public int ChannelId { get; set; }
        public GetConnectionDto Channel { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public GetEmployeeDto Employee { get; set; } = null!;
    }
}

