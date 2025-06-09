using Microsoft.EntityFrameworkCore;
using Test_APBD_2.Data;
using Test_APBD_2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.MapControllers();
app.Run();