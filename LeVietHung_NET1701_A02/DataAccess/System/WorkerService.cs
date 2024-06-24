using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using DataAccess.SignalRHub;

public class WorkerService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<WorkerService> _logger;
    private readonly IHubContext<RoomHub> _hubContext;
    public WorkerService(IServiceScopeFactory scopeFactory, ILogger<WorkerService> logger, IHubContext<RoomHub> hubContext)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FuminiHotelManagementContext>();

                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
                var checkedOutRooms = context.BookingDetails.Include(r=> r.Room).Include(b=> b.BookingReservation)
                    .Where((bd => bd.EndDate < currentDate && bd.BookingReservation.BookingStatus == 0 && bd.Room.RoomStatus == 0))
                    .Select(bd => bd.Room)
                    .ToList();

                foreach (var room in checkedOutRooms)
                {
                    room.RoomStatus = 1;
                    await _hubContext.Clients.All.SendAsync("ReceiveRoomUpdate", room.RoomId, 1);
                }
                var bookingReserve = context.BookingDetails.Include(b=> b.BookingReservation).Where(a=> a.BookingReservation.BookingStatus == 0 && a.EndDate < currentDate)
                    .Select(bd => bd.BookingReservation).ToList();

                foreach (var booking in bookingReserve)
                {
                    booking.BookingStatus = 1;
                }
                await context.SaveChangesAsync(stoppingToken);
            }

            // Chạy mỗi phút
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
