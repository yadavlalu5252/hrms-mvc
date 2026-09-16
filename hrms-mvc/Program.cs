using hrms_mvc.Data;
using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session configuration
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Scoped dependency
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IEmployeeListService, EmployeeListService>();










var app = builder.Build();

//// Global exception handling
//app.UseExceptionHandler("/Error/Index");

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/NotFound");

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
)
.WithStaticAssets();

app.Run();