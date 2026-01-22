using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiPractice9.Data;

var builder = WebApplication.CreateBuilder(args);


// Add CORS policybuilder.Services.AddCors(options =>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});



builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.


var app = builder.Build();

// Configure the HTTP request pipeline.


// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
