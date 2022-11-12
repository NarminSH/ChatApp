using System;
using Domain.Common;

namespace Application.Dtos.BaseDtos
{
    public class BaseAuditableDto : BaseEntityDto, IMapFrom<BaseAuditableEntity>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

