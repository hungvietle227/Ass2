using BusinessObject.Models;
using DataAccess.SignalRHub;
using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddRazorPages();
builder.Services.AddHostedService<WorkerService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();
builder.Services.AddScoped<IBookingReservationRepository, BookingReservationRepository>();

builder.Services.AddDbContext<FuminiHotelManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB"));
});
//builder.Services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Login", ""));

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<RoomHub>("/roomHub");

app.Run();
