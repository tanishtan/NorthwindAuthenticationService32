using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindModelClassLibrary
{
    public class AuthenticationResponse
    {
        public string? FullName { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }

        public AuthenticationResponse(UserModel userDetail, string token)
        {
            Token = token;
            FullName = userDetail.FirstName + " " + userDetail.LastName;
            UserId = userDetail.UserId;
        }
        //{ "Fullname" : "xxxx", "UserId" : 99, "TOken":"EncToken" } -> this is our output json
    }
}
