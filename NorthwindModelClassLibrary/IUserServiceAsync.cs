using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindModelClassLibrary
{
    public interface IUserServiceAsync
    {
        Task<UserModel> AuthenticateAsync(AuthenticationRequest model);
        Task<UserModel> GetUserDetails(int UserId);
    }

    public class UserService : IUserServiceAsync
    {
        private List<UserModel> _users = new List<UserModel>
        {
            new UserModel{UserId=1,FirstName="Ruben",LastName="Gupta", RoleName="Admin", Email="ruben.gupta@email.com",Username="admin",Password="admin"},
            new UserModel{UserId=2,FirstName="Sharan",LastName="Purohit", RoleName="Operator", Email="sharan.purohit@email.com",Username="operator",Password="operator"}
        };

        

        public Task<UserModel> AuthenticateAsync(AuthenticationRequest model)
        {
            var user = _users.FirstOrDefault(c => c.Username == model.Username && c.Password == model.Password);
            
            return Task.Run(() => user);
        }

        public Task<UserModel> GetUserDetails(int UserId)
        {
            var user = _users.FirstOrDefault(c => c.UserId==UserId);
            
            return Task.Run(() => user);
        }
    }
}
