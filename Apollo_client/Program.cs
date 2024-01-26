using Com.Ctrip.Framework.Apollo.Enums;
using Com.Ctrip.Framework.Apollo;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddApollo(builder.Configuration.GetSection("apollo"))
    .AddNamespace("Ocelot", ConfigFileFormat.Json).AddDefault();

builder.Services.AddOcelot();

var  de= builder.Configuration.GetSection("Name");
Console.WriteLine(de.Value);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
