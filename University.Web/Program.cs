using University.Data;
using Microsoft.EntityFrameworkCore;
using University.Services;
using Microsoft.Extensions.Configuration;
using University.Services.Services;
using System.Web.Mvc;
using University.Web;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DbContext, UniversityDbContext>();

builder.Services.AddScoped<UniversityService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddScoped<JwtCreditService>();
builder.Services.AddScoped<AuthenitcationService>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<UniversityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("University"));
});

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.UseAuthentiation();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
