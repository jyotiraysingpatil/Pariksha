using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pariksha.Repository;
using Pariksha.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories and services for DI (Dependency Injection)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IQuizeRepository, QuizzeRepository>();
builder.Services.AddScoped<IQuizzeService, QuizzeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()  
                   .AllowAnyMethod()  
                   .AllowAnyHeader();  
        });
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); // Swagger API endpoint
        options.RoutePrefix = string.Empty; // Make Swagger available at the root URL
        options.DocumentTitle = "My Swagger";  // Swagger UI page title
    });
}

// Enable CORS to allow cross-origin requests
app.UseCors("AllowAll");

// HTTPS redirection to ensure secure connections
app.UseHttpsRedirection();


app.UseAuthentication();  
app.UseAuthorization();  
app.MapControllers();


app.Run();
