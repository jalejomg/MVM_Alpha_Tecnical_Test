using Alpha.Web.API.Constants;
using Alpha.Web.API.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Web.API.Domain.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string State { get; set; }

        public static UserModel MakeOne(AspNetUser userEntity)
        {
            return new UserModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                Role = userEntity.Role,
                State = userEntity.State ? EntityStatus.Exists : EntityStatus.Deleted
            };
        }

        public static IEnumerable<UserModel> MakeMany(IEnumerable<AspNetUser> userEntities)
        {
            return userEntities.Select(userEntity => MakeOne(userEntity));
        }

        public static AspNetUser FillUp(UserModel userModel)
        {
            return new AspNetUser
            {
                Id = userModel.Id,
                Name = userModel.Name,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Role = userModel.Role
            };
        }
    }
}
