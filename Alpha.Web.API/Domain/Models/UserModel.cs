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

        public static UserModel MakeOne(User userEntity)
        {
            return new UserModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                Role = userEntity.Role,
                State = userEntity.State ? EntityStatus.Persistent : EntityStatus.Deleted
            };
        }

        public static IEnumerable<UserModel> MakeMany(IEnumerable<User> userEntities)
        {
            return userEntities.Select(userEntity => MakeOne(userEntity));
        }

        public static User FillUp(UserModel userModel)
        {
            return new User
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
