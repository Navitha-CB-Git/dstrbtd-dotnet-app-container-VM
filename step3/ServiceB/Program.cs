using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder => tracerProviderBuilder
    .ConfigureResource(resourceBuilder => resourceBuilder.AddService("ServiceB"))
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddSource("ServiceB")
    .AddJaegerExporter(options => options.ExportProcessorType = ExportProcessorType.Simple)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();