using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using Repository;
using LeVietHungRazorPages.Helper;
using Microsoft.AspNetCore.SignalR;
using DataAccess.SignalRHub;

namespace LeVietHungRazorPages.Pages.RoomManagement
{
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        public string SearchString { get; set; }

        public Customer Customer { get; set; }

        public IndexModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IList<RoomInformation> RoomInformation { get; set; } = default!;

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                RoomInformation = _roomRepository.Search(SearchString);
            }
            else
            {
                RoomInformation = _roomRepository.GetAllRoom().ToList();
                Customer = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "Customer");
            }
            return Page();
        }
    }
}
