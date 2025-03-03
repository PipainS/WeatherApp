
using DynamicSun.Weather.WebUI.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// ����� ������ ��� ������������ ��������
ApplicationContext.ConfigureServices(builder.Services, configuration);

var app = builder.Build();

// ����� ������ ��� ������������ ����������
ApplicationContext.Configure(app, app.Environment);

app.Run();
