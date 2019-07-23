using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HackatonBtp.WebApi.Filters
{
    public class SqlExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult("Ocorreu um erro ao processar seu pedido.");
        }
    }
}