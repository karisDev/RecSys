// <auto-generated/>

using System.Reflection;
using MediatR;
using RecSys.Api.Infrastructure;
using RecSys.Api.Jobs;
using RecSys.Customs.Client;
using RecSys.Platform.Data.Extensions;
using RecSys.Platform.Data.FluentMigrator;
using RecSys.Platform.Extensions;
using RecSys.Platform.Middlewares;

var builder = WebApplication
    .CreateBuilder(args)
    .UsePlatform();
var services = builder.Services;
var configuration = builder.Configuration;

#region DI

services.AddHttpClient(nameof(CustomsClient), client => client.BaseAddress = new Uri("http://stat.customs.gov.ru/"));
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwagger("rec-sys-api", useJwtAuth: true);
services.AddSerilogLogger();
services.AddPostgres();
services.AddScoped<CustomsClient>();
services.AddSingleton<CustomsDataCollectingProcessor>();
services.AddSingleton<DataProcessingProcessor>();
services.AddHostedService<MainHostedService>();
services.AddMigrator(typeof(Program).Assembly);
services.AddMediatR(typeof(Program));

#endregion

var app = builder.Build();

#region App

ExceptionMiddleware.ReturnStackTrace = false;
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(
    x =>
    {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
    });
app.MapControllers();

#endregion

await app.RunOrMigrateAsync(args);
