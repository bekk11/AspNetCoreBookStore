using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!IsSwagger(context))
        {
            string traceId = context.TraceIdentifier;
            string schema = context.Request.Scheme;
            string method = context.Request.Method;
            string host = context.Request.Host.Value;
            string queryString = context.Request.QueryString.ToString();
            string? clientIp = context.Connection.RemoteIpAddress?.ToString();
            string requestBody = await GetRequestBodyAsync(context.Request);

            _logger.LogInformation(
                "REQUEST INFO:  [TraceId: {TraceId}] - Schema: {Schema} - Method: {Method} - Host: {Host} - Path: {RequestPath} - QueryString: {QueryString} - Client IP: {CLientIp} - RequestBody: {RequestBody}",
                traceId, schema, method, host, context.Request.Path, queryString, clientIp,
                requestBody.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("  ", " "));

            // Copy the response stream to capture the response body
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                Stopwatch stopwatch = Stopwatch.StartNew();
                await _next(context);
                stopwatch.Stop();

                // Capture the response body
                string responseBodyString = await GetResponseBodyAsync(context.Response);

                // Log response information
                string statusCode = context.Response.StatusCode.ToString();
                _logger.LogInformation(
                    "RESPONSE INFO: [TraceId: {TraceId}] - Status: {StatusCode} - SpentTime: {elapsedTime}ms - Response Body: {ResponseBodyString}",
                    traceId, statusCode, stopwatch.ElapsedMilliseconds.ToString(), responseBodyString);

                // Copy the response body to the original stream for client consumption
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
        else
        {
            await _next(context);
        }
    }

    private bool IsSwagger(HttpContext context)
    {
        return context.Request.Path.ToString().ToLower().Contains("/swagger-ui") ||
               context.Request.Path.ToString().ToLower().Contains("/swagger") ||
               context.Request.Path.ToString().ToLower().Contains("/favicon") ||
               context.Request.Path.ToString().ToLower().Contains("/index.html") ||
               context.Request.Path.ToString().ToLower().Contains("/index.html") ||
               context.Request.Path.ToString().ToLower().Contains("/api/bank/list") ||
               context.Request.Path.ToString().ToLower().Contains("/api/Filials/list");
    }

    private async Task<string> GetRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();

        using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8,
                   detectEncodingFromByteOrderMarks: true, leaveOpen: true))
        {
            string requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0; // Reset the request body position for further processing.
            return requestBody;
        }
    }

    private async Task<string> GetResponseBodyAsync(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        string responseBody =
            await new StreamReader(response.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true)
                .ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin); // Reset the response body position for further processing.
        return responseBody;
    }
}