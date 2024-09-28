using CityManagerApi_aspls13.Entities;

namespace CityManagerApi_aspls13.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
