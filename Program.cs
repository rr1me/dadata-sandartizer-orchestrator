using dadataStandartizer.ExceptionHandlers;
using dadataStandartizer.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(x => { x.AddFile("app.log", x => x.MinLevel = LogLevel.Debug); });

builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddScoped<RequestHandler>();
builder.Services.AddTransient<ErrorResponse>();

builder.Services.AddCors(x => x.AddDefaultPolicy(x =>
{
    var corsSection = builder.Configuration.GetSection("Cors");

    var origins = corsSection.GetSection("Origins").GetChildren().Select(x => x.Value).ToArray();
    x.WithOrigins(origins);

    var headers = corsSection.GetSection("Headers").GetChildren().Select(x => x.Value).ToArray();
    x.WithHeaders(headers);

    var methods = corsSection.GetSection("Methods").GetChildren().Select(x => x.Value).ToArray();
    x.WithMethods(methods);
}));

var app = builder.Build();

app.UseCors();

app.UseHttpsRedirection();

app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Use(async (context, next) =>
{
    var logger = app.Logger;

    var request = context.Request;

    var requestPath = request.Path;
    var requestMethod = request.Method;
    logger.LogInformation($"Starting: {requestPath} | Method: {requestMethod}");

    await next.Invoke();

    var responseStatusCode = context.Response.StatusCode;
    logger.LogInformation($"Ending: {requestPath} | Code: {responseStatusCode}");
});

app.Run();