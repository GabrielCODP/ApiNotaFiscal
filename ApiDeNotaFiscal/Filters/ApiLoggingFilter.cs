using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiDeNotaFiscal.Filters
{
    public class ApiLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //antes da action executa
            _logger.LogInformation("##### Executando antes da action");
            _logger.LogInformation("##################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("##################################");

           var resultContext = await next();

            if (resultContext.Exception != null && !resultContext.ExceptionHandled)
            {
                _logger.LogError(resultContext.Exception, "Error durante a execução da action");
            }

            //depois da action executa
            _logger.LogInformation("##### Executando depois da action");
            _logger.LogInformation("##################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"Status code: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("##################################");
        }
    }
}
