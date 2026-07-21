using Core.Mapping;
using Core.Repository;
using Core.Services;
using Data; 
using Microsoft.EntityFrameworkCore;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();

// רישום ה-DbContext וקישור למחרוזת החיבור מה-appsettings.json
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Gmach")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// רישום AutoMapper במערכת
builder.Services.AddAutoMapper(typeof(MappingProfile)); 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<Core.Services.IUserService, Service.UserService>(); builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularApp");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


