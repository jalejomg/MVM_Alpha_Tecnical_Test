using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Web.API.Data.Entities
{
    public class AspNetUser : IdentityUser, IEntity<string>
    {
        [Required]
        public bool State { get; set; }
        [InverseProperty(nameof(Message.Sender))]
        public ICollection<Message> MessagesAsSender { get; set; }

        [InverseProperty(nameof(Message.Addressee))]
        public ICollection<Message> MessagesAsAddressee { get; set; }
    }
}
