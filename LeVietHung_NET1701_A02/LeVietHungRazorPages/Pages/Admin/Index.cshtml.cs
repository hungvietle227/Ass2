using BusinessObject.Models;
using LeVietHungRazorPages.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;

namespace LeVietHungRazorPages.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        public IndexModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IList<RoomInformation> RoomInformation { get; set; } = default!;

        public void OnGet()
        {
            RoomInformation = _roomRepository.GetAllRoom().ToList();
        }
    }
}
