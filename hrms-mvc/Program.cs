using hrms_mvc.Data;
using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.EntityFrameworkCore;
using hrms_mvc.Repository.EventsRepo;
using hrms_mvc.Repository.ProjectsRepo;
using hrms_mvc.Services;
using hrms_mvc.Services.EventsService;
using hrms_mvc.Services.ProjectsService;
using Microsoft.EntityFrameworkCore;
using hrms_mvc.Repository.TaskRepo;
using hrms_mvc.Services.TaskService;
using hrms_mvc.Repository.TaskRepo;
using hrms_mvc.Services.TaskService;

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

builder.Services
    .AddAuthentication()
    .AddCookie("GoogleCookie")
    .AddGoogle(options =>
    {
        options.ClientId =
            builder.Configuration["Authentication:Google:ClientId"]!;

        options.ClientSecret =
            builder.Configuration["Authentication:Google:ClientSecret"]!;

        options.SignInScheme = "GoogleCookie";
    });


// Scoped dependency
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IResignationService, ResignationService>();
builder.Services.AddScoped<ITerminationService, TerminationService>();
builder.Services.AddScoped<ITicketsService, TicketsService>();




builder.Services.AddScoped<IResignationService, ResignationService>();

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
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IProjects, ProjectsService>();
builder.Services.AddScoped<ITask, TaskService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();


builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ITraininglistService, TraininglistService>();





var app = builder.Build();



// Global exception handling
app.UseExceptionHandler("/Error/Index");

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/NotFound");

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
)
.WithStaticAssets();

app.Run();
