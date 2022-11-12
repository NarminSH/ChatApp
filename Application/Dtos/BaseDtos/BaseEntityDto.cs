using System;
using Domain.Common;

namespace Application.Dtos.BaseDtos
{
    public class BaseEntityDto : IMapFrom<BaseEntity>
    {
        public int Id { get; set; }
    }
}

