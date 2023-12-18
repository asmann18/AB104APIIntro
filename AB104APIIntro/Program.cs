using AB104APIIntro.DAL;
using AB104APIIntro.Repositories.Implementations;
using AB104APIIntro.Repositories.Interfaces;
using AB104APIIntro.Services.Implmentations;
using AB104APIIntro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ITagRepository,TagRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();   
builder.Services.AddScoped<ITagService, TagService>();   

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
