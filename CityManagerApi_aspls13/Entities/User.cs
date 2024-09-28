﻿namespace CityManagerApi_aspls13.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public List<City> ?Cities { get; set; }
    }
}
