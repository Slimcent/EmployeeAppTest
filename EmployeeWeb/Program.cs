using Employee.IntegrationTest.Infrastructures;
using EmployeeWeb.Interfaces;
using EmployeeWeb.Middlewares;
using EmployeeWeb.Repository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(t =>
{
    t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
    t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
});
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

//app.MigrateDatabase();
app.Run();

public partial class Program { }
