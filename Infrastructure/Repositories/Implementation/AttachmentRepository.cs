using System;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Implementation;

namespace Infrastructure.Repositories.Implementation
{
    public class AttachmentRepository : GenericRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

