using DynamicSun.Weather.WebUI.Context;

#region Build and Configure Services
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

ApplicationContext.ConfigureServices(builder.Services, configuration);
#endregion

#region Build Application
var app = builder.Build();
#endregion

#region Configure Application
ApplicationContext.Configure(app, app.Environment);
#endregion

#region Run Application
app.Run();
#endregion