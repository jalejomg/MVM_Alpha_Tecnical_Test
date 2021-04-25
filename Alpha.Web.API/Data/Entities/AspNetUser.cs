using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Web.API.Data.Entities
{
    public class AspNetUser : IdentityUser, IEntity<string>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(20)]
        [Required]
        public string Role { get; set; }

        [Required]
        public bool State { get; set; }

        [InverseProperty(nameof(Message.Sender))]
        public ICollection<Message> MessagesAsSender { get; set; }

        [InverseProperty(nameof(Message.Addressee))]
        public ICollection<Message> MessagesAsAddressee { get; set; }
    }
}
