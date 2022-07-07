using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View(Hotel.Rooms);
        }
        public IActionResult TotalCapacity()
        {
            ViewBag.Message1 = Hotel.TotalCapacityRemaining();
            ViewBag.Message2 = Hotel.Rooms.Where(r => !r.Occupied).ToList().Count;
            ViewBag.Message3 = "No more than 25 employees should be in this building, according to local fire codes."; // if the last 5 rooms in the Hotel Class are commented out, This viewbag gets displayed.
            return View(Hotel.Rooms);
        }

        public IActionResult GetCapacity(int? occupants)
        {
            if(occupants != null)
            {
                try
                {
                    Room room = Hotel.Rooms.OrderBy(r => r.Capacity).ToList().First(r => r.Capacity >= occupants);
                    return View(room);
                }
                catch
                {
                    return RedirectToAction("NoRoomError");
                }
            }
            else 
            { 
                return RedirectToAction("CapacityInputError");
            }
        }
        public IActionResult NoRoomError()
        {
            ViewBag.Message = "Error: No room is available with this capacity";
            return View();
        }

        public IActionResult CapacityInputError()
        {
            ViewBag.Message = "Error: occupant can not be null, please check your URL";
            return View();
        }
    }
}
