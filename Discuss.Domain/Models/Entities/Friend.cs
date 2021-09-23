using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discuss.Domain.Models.Entities
{
    public class Friend
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        public string UserId { get; set; }
        public string FriendlyId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("FriendlyId")]
        public virtual User Friendly { get; set; }
    }
}