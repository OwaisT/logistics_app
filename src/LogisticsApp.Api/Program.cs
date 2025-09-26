using LogisticsApp.Api;
using LogisticsApp.Application;
using LogisticsApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();


// TODO: make sure to change the secret key in appsettings.json before deploying the application
// TODO: make sure to change the database connection string in appsettings.json before deploying the application


var app = builder.Build();
{
    // app.UseSwagger();
    // app.UseSwaggerUI();

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
