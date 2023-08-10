using M7CarManager.Data;
using M7CarManager.Hubs;
using M7CarManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace M7CarManager.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        IHubContext<EventHub> hub;
        ApiDbContext db;
        UserManager<AppUser> _userManager;

        public CarController(IHubContext<EventHub> hub, ApiDbContext db, UserManager<AppUser> userManager)
        {
            this.hub = hub;
            this.db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<Car> GetCars()
        {
            return db.Cars;
        }

        [HttpGet("{id}")]
        [Authorize]
        public Car? GetCars(string id)
        {
            return db.Cars.FirstOrDefault(t => t.Id == id);
        }

        [Route("[action]")]
        [Authorize]
        [HttpPost]
        public async void AddCar([FromBody] Car c)
        {
            var user = _userManager.Users.FirstOrDefault
                (t => t.UserName == this.User.Identity.Name);
            c.OwnerId = user.Id;
            c.Id = Guid.NewGuid().ToString();
            db.Cars.Add(CarHelper(c));
            db.SaveChanges();
            await hub.Clients.All.SendAsync("carCreated", c);
        }

        private Car CarHelper(Car c)
        {
            if (c.PlateNumber.Length != 7)
            {
                throw new ArgumentException("Platenumber format is invalid...");
            }
            else
            {
                return c;
            }
        }


        [HttpPut]
        public async Task<IActionResult> EditCar([FromBody] Car c)
        {
            var userName = this.User.Identity.Name;
            var old = db.Cars.FirstOrDefault(t => t.Id == c.Id);
            if (old.Owner.UserName == userName)
            {
                old.Model = c.Model;
                old.PlateNumber = c.PlateNumber;
                old.Price = c.Price;
                db.SaveChanges();
                await hub.Clients.All.SendAsync("carModified", old);
                return Ok();
            }
            else
            {
                throw new ArgumentException("Not your car");
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            var userName = this.User.Identity.Name;
            var carToDelete = db.Cars.FirstOrDefault(t => t.Id == id);
            if (carToDelete.Owner.UserName == userName)
            {
                db.Cars.Remove(carToDelete);
                db.SaveChanges();
                await hub.Clients.All.SendAsync("carDeleted", id);
                return Ok();
            }
            else
            {
                throw new ArgumentException("Not your car");
            }
            
        }

    }
}
