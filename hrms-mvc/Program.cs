using hrms_mvc.Data;
using hrms_mvc.Repository;
using hrms_mvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();




// scoped dependency
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ITrainingTypeService, TrainingTypeService>();
builder.Services.AddScoped<ITrainingListService, TrainingListService>();
builder.Services.AddScoped<IMasterDocAdminService, MasterDocAdminService>();
builder.Services.AddScoped<IMasterDocEmpService, MasterDocEmpService>();
builder.Services.AddScoped<IAdminFileUpload, AdminFileUploadService>();

builder.Services.AddScoped<IUploadedDocument, UploadedDocumentListService>();
builder.Services.AddScoped<IMyDocument, MyDocumentService>();
builder.Services.AddScoped<ICompanyLetters, CompanyLettersService>();
builder.Services.AddScoped<IEmployeeUploadDocument, EmployeeUploadDocumentService>();






var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
