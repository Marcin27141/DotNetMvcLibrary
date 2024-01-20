using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Database.Generators;
using LibraryApp.Models.Database.Generators.Books;
using LibraryApp.Models.Database.Generators.BooksCopies;
using LibraryApp.Models.Repositories.Accounts;
using LibraryApp.Models.Repositories.EmailSender;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Rentals;
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
    //for testing
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<LibraryDbContext>();

builder.Services.AddScoped<ILibraryEmailSender, LibraryEmailSender>();

builder.Services.AddScoped<IRenewalValidator, UnpaidPenaltiesValidator>();
builder.Services.AddScoped<IRenewalValidator, RenewalLimitValidator>();
builder.Services.AddScoped<IRenewalCreator, RenewalCreator>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDatabaseGenerator, DatabaseGenerator>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IRenewalRepository, RenewalRepository>();



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
