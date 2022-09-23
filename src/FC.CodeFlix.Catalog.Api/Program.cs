using FC.CodeFlix.Catalog.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services
      .AddUseCases()
      .AddAndConfigureControllers()
      .AddAppConnections();

var app = builder.Build();
app.UseDocumentation();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }