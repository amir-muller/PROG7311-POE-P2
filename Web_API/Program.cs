using Microsoft.EntityFrameworkCore;
using Web_API.Data;
using Web_API.Services;

var builder = WebApplication.CreateBuilder(args);

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVCApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//currency service
builder.Services.AddHttpClient(); 
builder.Services.AddScoped<CurrencyService>();

//ApplicationDBContext
builder.Services.AddDbContext<Web_API.Data.ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowMVCApp");

app.UseAuthorization();

app.MapControllers();

app.Run();