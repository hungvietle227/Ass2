using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace NguyenDoCaoLinhRazorPages.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        private readonly IRoomRepository _roomInformationRepository;
        public DeleteModel(IRoomRepository roomInformationRepository)
        {
            _roomInformationRepository = roomInformationRepository;
        }

        [BindProperty]
      public RoomInformation RoomInformation { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            var roominformation = _roomInformationRepository.GetRoomInfoByID(id.ToString());

            if (roominformation == null)
            {
                return NotFound();
            }
            else 
            {
                RoomInformation = roominformation;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            var roominformation = _roomInformationRepository.GetRoomInfoByID(id.ToString());

            if (roominformation != null)
            {
                RoomInformation = roominformation;
                var checkDeleteRoom = _roomInformationRepository.DeleteRoom(id);
                if (checkDeleteRoom)
                {
                    return RedirectToPage("/Admin/Index");
                }             
            }
            return NotFound();
        }
    }
}
