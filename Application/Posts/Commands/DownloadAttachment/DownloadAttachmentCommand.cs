using System;
using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Application.Repositories.Abstraction;
using AutoMapper;

namespace Application.Posts.Commands.DownloadAttachment
{
    public class DownloadAttachmentCommand: IRequest<ResponseMessage>, IMapFrom<Attachment>
    {
        public IFormFile File { get; set; } = null!;
        public int PostId { get; set; }
    }
    public class DownloadAttachmentCommandHandler : IRequestHandler<DownloadAttachmentCommand, ResponseMessage>
    {
        
        private readonly IPostRepository _postRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly IUploadFile _uploadFileService;
        public DownloadAttachmentCommandHandler(IUploadFile uploadFile,
            IPostRepository postRepository, IAttachmentRepository attachmentRepository,
            IMapper mapper)
        {
            _uploadFileService = uploadFile;
            _postRepository = postRepository;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<ResponseMessage> Handle(DownloadAttachmentCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.GetByIdInt(request.PostId);
            Tuple<string, string> result = _uploadFileService.SaveFile(request);
            string fileName = result.Item1;
            string fileType = result.Item2;
            Attachment attachment = _mapper.Map<Attachment>(request);
            attachment.FileName = fileName;
            attachment.Type = fileType;
            await _attachmentRepository.AddAsync(attachment);
            return new ResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = "Uploaded the file, successfully!"
            };
        }
    }
}

