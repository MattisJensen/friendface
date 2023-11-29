using FriendFace.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FriendFace.Data;
using FriendFace.Areas.Identity.Data;
using FriendFace.Services.DatabaseService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("FriendFaceIdentityDbContextConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// If you're using roles, use AddIdentity. Otherwise, you can use AddDefaultIdentity.
builder.Services.AddIdentity<User, IdentityRole<int>>() // Adjust IdentityRole<int> as per your setup
    .AddEntityFrameworkStores<FriendFaceIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CommentCreateService>();
builder.Services.AddScoped<CommentQueryService>();
builder.Services.AddScoped<PostDeleteService>();
builder.Services.AddScoped<PostCreateService>();
builder.Services.AddScoped<PostQueryService>();
builder.Services.AddScoped<PostUpdateService>();
builder.Services.AddScoped<UserCreateService>();
builder.Services.AddScoped<UserQueryService>();
builder.Services.AddScoped<UserUpdateService>();
builder.Services.AddScoped<PostService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();