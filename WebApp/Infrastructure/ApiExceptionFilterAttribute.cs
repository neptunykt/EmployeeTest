using System;
using System.Collections.Generic;
using Core.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebApp.Infrastructure
{
      /// <summary>
      /// 
      /// </summary>
      public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, Action<ExceptionContext>> _handlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
            _handlers = new Dictionary<Type, Action<ExceptionContext>>
            {
               
                {typeof(Exception), HandleException},
                {typeof(ValidationException), HandleValidationException},
                {typeof(ServiceException), HandleServiceException},
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            _logger.LogError(context.Exception, "Unhandled exception");
            foreach (var definition in _handlers)
            {
                if (definition.Key == context.Exception.GetType())
                {
                    definition.Value.Invoke(context);
                    return;
                }
            }

            context.Result = new StatusCodeResult(500);
        }

      
        
        private void HandleValidationException(ExceptionContext context)
        {
            var validationException = (ValidationException) context.Exception;
            context.Result = new ObjectResult(
               new ApiResult(GetValidationErrorMessage(validationException)))
            {
                StatusCode = 400
            };
        }
        private void HandleException(ExceptionContext context) =>
            context.Result =
                new ObjectResult(new ApiResult(context.Exception.Message)) {StatusCode = 500};
        
        
        private string GetValidationErrorMessage(ValidationException validationException)
        {
            var errors = new List<string>();
            if (validationException.Errors != null)
            {
                foreach (var item in validationException.Errors)
                {
                    errors.Add($"Error: {item.ErrorMessage}");
                }
            }
            
            return string.Join(", ", errors);
        }
        
            private void HandleServiceException(ExceptionContext context)
        {
            var serviceException = (ServiceException) context.Exception;
            var errorMessage = GetServiceErrorMessage(serviceException);
            switch (serviceException.ErrorCode)
            {
                case "AUTH":
                    context.Result =
                        new ObjectResult(new ApiResult(errorMessage))
                        {
                            StatusCode = 403
                        };
                    break;
                case "BAD_REQUEST":
                    context.Result =
                        new ObjectResult(new ApiResult(errorMessage))
                        {
                            StatusCode = 400
                        };
                    break;
                case "NOT_FOUND":
                    context.Result =
                        new ObjectResult(new ApiResult(errorMessage))
                        {
                            StatusCode = 404
                        };
                    break;
               
                default:
                    context.Result =
                        new ObjectResult(new ApiResult("ERROR"))
                        {
                            StatusCode = 500
                        };
                    break;
            }
        }
            
            private string GetServiceErrorMessage(ServiceException serviceException)
            {
                var errors = new List<string>();
                if (serviceException.Errors != null)
                {
                    foreach (var item in serviceException.Errors)
                    {
                        errors.Add(item.ToString());
                    }
                }
                else
                {
                    return "SEE ERROR BY ERROR CODE";
                }

                return string.Join(", ", errors);
            }
    }
}