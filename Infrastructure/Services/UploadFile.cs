using System;
using Application.Posts.Commands.DownloadAttachment;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class UploadFile : IUploadFile
    {
        private readonly IWebHostEnvironment _environment;
        public UploadFile(IWebHostEnvironment env)
        {
            _environment = env;
        }
        public Tuple<string, string> SaveFile(DownloadAttachmentCommand request)
        {
            string directoryPath = Path.Combine(_environment.ContentRootPath, "UploadedFiles");
            string filePath = Path.Combine(directoryPath, request.File.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                request.File.CopyTo(stream);
            }
            string extensionType = request.File.ContentType;
            Tuple<string, string> value = new Tuple<string, string>(filePath, extensionType); ;
            return value ;
        }
    }
}

