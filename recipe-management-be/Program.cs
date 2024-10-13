using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RecipeManagementSystem.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {   
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"] ?? ""))
    };
});

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>(); // Ensure this line is added

var app = builder.Build();

// Add the RequestLoggingMiddleware
// app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowReactApp");

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();

// New Middleware Class
// public class RequestLoggingMiddleware
// {
//     private readonly RequestDelegate _next;

//     public RequestLoggingMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task Invoke(HttpContext context)
//     {
//         // Log the request details
//         // Console.WriteLine($"Request Method: {context.Request.Method}");
//         // Console.WriteLine($"Request Path: {context.Request.Path}");
        
//         // Read and log the request body
//         context.Request.EnableBuffering(); // Enable buffering to read the body multiple times
//         var bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();
//         Console.WriteLine($"Request Body: {bodyAsText}");
        
//         // Reset the request body stream position
//         context.Request.Body.Position = 0;

//         await _next(context);
//     }
// }