using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiOrderApplication.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiOrderApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiOrderApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApiOrderApplicationContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApiOrderApplicationContext>();
    context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
}

app.UseAuthorization();
app.UseCors(options => options.AllowAnyOrigin());

app.MapControllers();

app.Run();



