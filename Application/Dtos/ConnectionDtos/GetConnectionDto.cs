using System;
namespace Application.Dtos.ConnectionDtos
{
    public class GetConnectionDto : IMapFrom<Connection>
    {
        public string Name { get; set; } = null!;
        public bool IsChannel { get; set; }
        public bool IsPrivate { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

    }
}

