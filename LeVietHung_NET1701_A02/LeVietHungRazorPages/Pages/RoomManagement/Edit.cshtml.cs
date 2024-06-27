using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using BusinessObject.Models;

namespace NguyenDoCaoLinhRazorPages.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly IRoomRepository _roomInformationRepository;

        public EditModel(IRoomRepository roomInformationRepository)
        {
            _roomInformationRepository = roomInformationRepository;
        }
        public ICollection<RoomType> RoomTypes { get; set; }
        [BindProperty]
        public RoomInformation RoomInformation { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            RoomTypes = _roomInformationRepository.GetAllRommTypes();
            var roominformation = _roomInformationRepository.GetRoomInfoByID(id.ToString());
            ; if (roominformation == null)
            {
                return NotFound();
            }
            RoomInformation = roominformation;
            ViewData["RoomTypeId"] = new SelectList(RoomTypes, "RoomTypeId", "RoomTypeName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
        {
            var room = _roomInformationRepository.UpdateRoom(RoomInformation);
            return RedirectToPage("/Admin/Index");
        }

        /*        private bool RoomInformationExists(int id)
                {
                  return (_context.RoomInformations?.Any(e => e.RoomId == id)).GetValueOrDefault();
                }*/
    }
}
