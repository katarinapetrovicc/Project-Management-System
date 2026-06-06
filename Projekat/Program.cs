using DataBaseContext;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//string databasePath = Path.Combine("C:\\Users\\Admin\\Desktop\\radnici", "radnici.db");
//string databasePath = @"C:\Users\Admin\Desktop\si2\radnici\radnici.db";
string databasePath = Path.Combine(builder.Environment.ContentRootPath, "radnici.db");
builder.Services.AddDbContext<DB_Context_Class>(options => options.UseSqlite($"Data Source={databasePath}"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();