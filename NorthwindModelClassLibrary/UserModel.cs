using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NorthwindModelClassLibrary
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
        public string? Username { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
    }
}
