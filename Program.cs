using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
    
builder.Services.AddRazorPages(
    options=>
    {
        options.Conventions.AuthorizePage("/Products", "Admin");
        options.Conventions.AllowAnonymousToPage("/Index"); //public pages
    });

builder.Services.ConfigureApplicationCookie(options=>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/AccessDenied";
});
builder.Services.AddScoped<IAddProducts, AddProductsService>();

builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpClient<ExchangeRatesService>();

builder.Services.AddScoped<StripeService>();


var app = builder.Build();
// Initialize roles
using (var scope  = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await InitializeRoles(services); // call the role initialization method
    await AssignAdminRole(services); // call the admin role assignment method
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

//Role initialization method
async Task InitializeRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = {"Admin", "User"};

    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
//Admin role-assignment method
async Task AssignAdminRole(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var adminEmail = "admin@products.com";

    // find the admin user
    var user = await userManager.FindByEmailAsync(adminEmail);
    if(user != null && !await userManager.IsInRoleAsync(user, "Admin"))
    {
        await userManager.AddToRoleAsync(user, "Admin");
    } 
}


