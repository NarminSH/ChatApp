using System;
using Application.Posts.Commands.DownloadAttachment;

namespace Application.Repositories.Abstraction
{
    public interface IUploadFile
    {
        Tuple<string, string> SaveFile(DownloadAttachmentCommand attachmentCommand);
    }
}

