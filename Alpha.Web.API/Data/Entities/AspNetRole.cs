using Microsoft.AspNetCore.Identity;

namespace Alpha.Web.API.Data.Entities
{
    public class AspNetRole : IdentityRole, IEntity<string>
    {
    }
}
