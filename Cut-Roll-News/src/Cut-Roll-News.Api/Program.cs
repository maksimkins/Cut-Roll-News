using Cut_Roll_News.Api.Common.Extensions.ServiceCollection;
using Cut_Roll_News.Api.Common.Extensions.WebApplication;
using Cut_Roll_News.Api.Common.Extensions.WebApplicationBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.SetupVariables();
builder.Services.InitDbContext(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UpdateDb();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
