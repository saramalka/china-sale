﻿namespace server.Models.DTO
{
    public class UserDtoTheen
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? phone { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
    }
}
