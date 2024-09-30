using PA.Api;
using PA.BW;
using PA.GqClient;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
});

builder.Services.AddApiServices();
builder.Services.AddGqClient(builder.Configuration);
builder.Services.AddBWServices();
builder.Services.ConfigureMasstransit(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
    string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);
    foreach (string filePath in files)
    {
        options.IncludeXmlComments(filePath, includeControllerXmlComments: true);
    }
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commands API V1");
});

app.UseSerilogRequestLogging();
app.UseWebApi();

app.Run();