using LoanMe.Api;

var builder = WebApplication.CreateBuilder(args);
DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
DependencyInjection.ConfigureApplication(app, app.Environment);

app.Run();
