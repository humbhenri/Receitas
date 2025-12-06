using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBookAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MyRecipeBookException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException exception)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            var errors = exception.ErrorMessages;
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(errors));
        }
        else if (context.Exception is InvalidLoginException loginException)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(loginException.Message));
        }
    }

    private static void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(Messages.unknown_error));
    }
}