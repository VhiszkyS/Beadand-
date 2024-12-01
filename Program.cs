using Beadandó;
using Beadandó.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("http://localhost:5130")  // Blazor alkalmazás portja
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookContext>(options =>
{
   options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));

});
builder.Services.AddDbContext<ReaderContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));

});
builder.Services.AddDbContext<LoanContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));

});


builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<ILoanService, LoanService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
