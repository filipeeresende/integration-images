using BonifiQ.Application.Tools.ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BonifiQ.Application.v1.Controller.Base
{
    [ApiController]
    [ValidateModelState]
    public class MainController : ControllerBase
    {
    }
}
