using LibraryApp.Extensions;
using LibraryApp.Models.Accounts.AccountValidator;
using LibraryApp.Models.Accounts.AccountVerifiers;
using LibraryApp.Models.Accounts.Contracts;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Database.Generators.Contracts;
using LibraryApp.Models.Database.Generators.DatabaseGenerators;
using LibraryApp.Models.EmailSender;
using LibraryApp.Models.Renewals.Contracts;
using LibraryApp.Models.Renewals.RenewalCreator;
using LibraryApp.Models.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Accounts;
using LibraryApp.Models.Repositories.Readers;
using LibraryApp.Models.Repositories.Renewals.Contracts;
using LibraryApp.Models.Repositories.Renewals.RenewalRepositories;
using LibraryApp.Models.Repositories.Rentals;
using LibraryApp.Models.Specifications.Passwords;
using LibraryApp.Models.Specifications.RenewalSpecification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDefaultIdentity<LibraryUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    //for custom password policy handling
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<LibraryDbContext>();

builder.Services.AddScoped<ILibraryEmailSender, LibraryEmailSender>();

builder.Services.AddSpecifications();
builder.Services.AddAccountValidation();

builder.Services.AddRenewalValidators();
builder.Services.AddScoped<IRenewalCreator, RenewalCreator>();

builder.Services.AddLibraryRepositories();

builder.Services.AddScoped<IDatabaseGenerator, DatabaseGenerator>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("IsReader", policy =>
        policy.RequireRole("Reader"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    //seeding database
    using (var serviceScope = app.Services.CreateScope())
    {
        var databaseGenerator = serviceScope.ServiceProvider.GetRequiredService<IDatabaseGenerator>();
        databaseGenerator?.SeedTables();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
