using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebUI.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        private ISender _mediator = null;
        public ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}

