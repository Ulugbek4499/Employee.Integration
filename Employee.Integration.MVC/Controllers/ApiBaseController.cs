using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Integration.MVC.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApiBaseController : Controller
    {
        private IMediator? _mediator;
        public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
        protected IWebHostEnvironment _hostEnviroment
       => HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
    }
}
