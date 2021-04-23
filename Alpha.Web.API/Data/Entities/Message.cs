using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Web.API.Data.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Body { get; set; }

        [Required]
        public short Type { get; set; }
        public short DeliveryStatus { get; set; }

        [ForeignKey("AddresseeId")]
        public User Addressee { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [Required]
        public bool State { get; set; }
    }
}
