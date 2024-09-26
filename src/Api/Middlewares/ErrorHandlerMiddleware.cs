using Amazon.Auth.AccessControlPolicy;
using Domain.Exceptions;
using Domain.Wrappers;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IStringLocalizer<Resource> _stringLocalizer;
    public ErrorHandlerMiddleware(IStringLocalizer<Resource> stringLocalizer, RequestDelegate next)
    {
        _stringLocalizer = stringLocalizer;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

            switch (error)
            {
                case ApiException e:
                    if (e.SendLogger)
                    {
                        Log.Warning(e, "Warning");
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    if (e.Errors != null && e.Errors.Count > 0)
                        responseModel.Errors = e.Errors;

                    if (!string.IsNullOrWhiteSpace(e.CodeApiError))
                    {
                        string messageResouce = _stringLocalizer[e.CodeApiError.ToString()];

                        if (e.ConcatenatedString.Any())
                        {
                            messageResouce = string.Format(messageResouce, e.ConcatenatedString.ToArray());

                            responseModel.Message = messageResouce;
                        }
                    }
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = new();
                    foreach (var erro in e.Errors)
                    {
                        string errorResult = erro;
                        responseModel.Errors.Add(errorResult);
                    }
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    Log.Fatal(e, "not found error");
                    responseModel.Message = string.Concat(_stringLocalizer["_not_found_error"], ": ", responseModel.Message);
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Log.Fatal(error, "unhandled error");
                    responseModel.Message = string.Concat(_stringLocalizer["_unhandled_error"], ": ", responseModel.Message);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(responseModel.Message) && responseModel.Message.IndexOf('_') > -1)
            {
                responseModel.Message = _stringLocalizer[responseModel.Message];
            }

            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}