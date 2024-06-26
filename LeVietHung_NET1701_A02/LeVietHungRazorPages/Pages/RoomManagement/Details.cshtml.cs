﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace LeVietHungRazorPages.Pages.RoomManagement
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObject.Models.FuminiHotelManagementContext _context;

        public DetailsModel(BusinessObject.Models.FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public RoomInformation RoomInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roominformation = await _context.RoomInformations.FirstOrDefaultAsync(m => m.RoomId == id);
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
    }
}
