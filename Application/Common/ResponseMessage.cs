﻿using System;
using System.Net;

namespace Application.Common
{
    public class ResponseMessage
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}

