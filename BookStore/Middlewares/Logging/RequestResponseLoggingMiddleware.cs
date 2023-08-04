using System.Diagnostics;
using Microsoft.IO;

namespace BookStore.Middlewares.Logging;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
    private Stopwatch _stopwatch = new Stopwatch();
    
    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task Invoke(HttpContext context)
    {
        _stopwatch.Start();
        await LogRequest(context);
        await _next(context);
        await LogResponse(context);
        _stopwatch.Stop();
    }

    private async Task LogRequest(HttpContext context)
    {
        await using var requestStream = _recyclableMemoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);
        
        _logger.LogInformation(
            "REQUEST INFO: {TraceIdentifier} - {Schema} - {Host} - {Url} - {ClientIP} - {QueryString} - {RequestBody}",
            context.TraceIdentifier,
            context.Request.Scheme,
            context.Request.Host.Value,
            context.Request.Path.Value,
            context.Connection.RemoteIpAddress,
            context.Request.QueryString.Value,
            ReadStreamInChunks(requestStream).Replace("\r\n", "").Replace("\r", "").Replace("\n", "")
        );
    }
    
    private async Task LogResponse(HttpContext context)
    {
        await using var requestStream = _recyclableMemoryStreamManager.GetStream();
        await context.Response.Body.CopyToAsync(requestStream);
        
        _logger.LogInformation(
            "RESPONSE INFO: {TraceIdentifier} - {Schema} - {Path} - {QueryString} - {ResponseBody}",
            context.TraceIdentifier,
            context.Request.Scheme,
            context.Request.Path.Value,
            context.Request.QueryString.Value,
            ReadStreamInChunks(requestStream).Replace("\r\n", "").Replace("\r", "").Replace("\n", "")
        );
    }
    
    private static string ReadStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;

        stream.Seek(0, SeekOrigin.Begin);

        using var textWriter = new StringWriter();
        using var reader = new StreamReader(stream);

        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;

        do
        {
            readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);

        return textWriter.ToString();
    }
}