var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var temporalHost = builder.Configuration.GetValue<string>("Temporal:Host");
var temporalNameSpace = builder.Configuration.GetValue<string>("Temporal:Namespace");

builder.Services.AddTemporalClient(temporalHost, temporalNameSpace);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
