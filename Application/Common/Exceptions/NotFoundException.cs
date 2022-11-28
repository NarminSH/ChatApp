using System;
namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Entity was not found") { }
        public NotFoundException(string message) : base(message) { }
    }
}

