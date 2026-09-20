using hrms_mvc.Data;
using hrms_mvc.Repository;
using hrms_mvc.Repository.EventsRepo;
using hrms_mvc.Services;
using hrms_mvc.Services.EventsService;
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
builder.Services.AddScoped<IEmployeeReportService, EmployeeReportService>();
builder.Services.AddScoped<IAttendanceReportService, AttendanceReportService>();
builder.Services.AddScoped<ILeaveReportService, LeaveReportService>();
builder.Services.AddScoped<IEvents, EventsService>();
builder.Services.AddScoped<IEmployeeDetailsService, EmployeeDetailsService>();
builder.Services.AddScoped<IProjectReportService, ProjectReportService>();
builder.Services.AddScoped<ITaskReportService, TaskReportService>();
builder.Services.AddScoped<IDailyReportService, DailyReportService>();
builder.Services.AddScoped<IPaySlipReportService, PaySlipReportService>();







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