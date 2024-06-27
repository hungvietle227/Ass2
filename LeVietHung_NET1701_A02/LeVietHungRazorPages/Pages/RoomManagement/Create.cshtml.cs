using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;

namespace NguyenDoCaoLinhRazorPages.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly IRoomRepository _roomInformationRepository;
        public CreateModel(IRoomRepository roomInformationRepository)
        {
            _roomInformationRepository = roomInformationRepository;
        }
        public ICollection<RoomType> RoomTypes{ get; set; }
        public async Task<IActionResult> OnGet()
        {
            RoomTypes = _roomInformationRepository.GetAllRommTypes();
            ViewData["RoomTypeId"] = new SelectList(RoomTypes, "RoomTypeId", "RoomTypeName");
            return Page();
        }

        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (RoomInformation == null)
          {
                return Page();
          }

            var room = _roomInformationRepository.CreateRoom(RoomInformation);

            return RedirectToPage("/Admin/Index");
        }
    }
}
