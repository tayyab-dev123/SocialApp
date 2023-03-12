using System.ComponentModel.DataAnnotations.Schema;

namespace SocialApp.Entities
{
    [Table("Photos")] // Name of the table will be photos in DB
    public class Photo
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}