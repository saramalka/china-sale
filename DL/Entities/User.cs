using common.enums;
using System.Net.Sockets;
namespace DL.Entities
{
   
        public class User
        {
            public int Id { get; set; }
            public string Fullname { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string? phone { get; set; }
            public UserRole Role { get; set; } = UserRole.User;

            public List<Ticket> Tickets { get; set; }
        }

    
}