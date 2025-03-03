
using DynamicSun.Weather.WebUI.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Вызов метода для конфигурации сервисов
ApplicationContext.ConfigureServices(builder.Services, configuration);

var app = builder.Build();

// Вызов метода для конфигурации приложения
ApplicationContext.Configure(app, app.Environment);

app.Run();
