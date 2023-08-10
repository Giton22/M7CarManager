using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M7CarManager.Models
{
    public class Car
    {
        [Key]
        public string Id { get; set; }
        public string Model { get; set; }

        public string PlateNumber { get; set; }
        public int Price { get; set; }

        public string? OwnerId { get; set; }

        [NotMapped]
        public virtual AppUser? Owner { get; set; }

        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
