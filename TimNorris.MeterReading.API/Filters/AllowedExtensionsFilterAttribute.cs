using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TimNorris_MeterReadings.Api.Client.Requests;

namespace TimNorris.MeterReading.API.Filters
{
    public class AllowedExtensionsFilterAttribute : Attribute, IActionFilter
    {
        public string[]? Extensions { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (Extensions == null)
                return;

            var request = context.ActionArguments.SingleOrDefault(p => p.Value is UploadMeterReadingsRequest);
            if (request.Value == null || ((UploadMeterReadingsRequest)request.Value).File == null)
            {
                context.Result = new BadRequestObjectResult("Invalid request");
                return;
            }

            var extension = Path.GetExtension(((UploadMeterReadingsRequest)request.Value).File?.FileName ?? String.Empty);
            if (!Extensions.Contains(extension.ToLower()))
            {
                context.Result = new BadRequestObjectResult(GetErrorMessage(extension.ToLower()));
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public string GetErrorMessage(string extension)
        {
            return $"File extension not allowed: {extension}";
        }
    }
}
