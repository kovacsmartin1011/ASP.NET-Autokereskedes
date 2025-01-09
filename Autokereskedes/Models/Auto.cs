using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autokereskedes.Models
{
    public enum Brands
    {
        Audi,
        BMW,
        Mercedes,
        Volkswagen,
        Toyota,
        Honda,
        Ford,
        Chevrolet,
        Nissan,
        Hyundai,
        Kia,
        Subaru,
        Volvo,
        Mazda,
        Renault,
        Peugeot,
        Citroën,
        Suzuki,
        Mitsubishi,
        Tesla,
        Ferrari,
        Lamborghini,
        Porsche,
        Bugatti,
        Maserati,
        Alfa_Romeo,
        Jaguar,
        Land_Rover,
        Lexus,
        Infiniti
    }
    public enum FuelType
    {
       Benzin,
       Gázolaj,
       Hibrid
    }
    public enum BodyType
    {
        Sedan,
        Coupe,
        Sport,
        SUV,
        Hatchback,
        Kombi,
        Egyterű,
        Terepjáró,
        Pickup,
        Kisbusz,
        Cabrio,
    }
    public class Auto
    {
        public int Id { get; set; }

        [Required]
        public Brands Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public string Color { get; set; }

        [Required]
        public int Year {  get; set; }

        [Required]
        public FuelType Fuel_Type { get; set; }

        [Required]
        public BodyType Body_Type { get; set; }

        [Required]
        public string Condition { get; set; }

        public string Extras { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Capacity { get; set; }
        public string Description { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public string? UserId { get; set; } // null-t is felvehet

    }
}
