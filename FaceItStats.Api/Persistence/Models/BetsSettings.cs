using System.ComponentModel.DataAnnotations;

namespace FaceItStats.Api.Persistence.Models
{
    public class BetsSettings
    {
        [Key]
        public int Id { get; set; }

        public bool IsEnabled { get; set; }
    }
}
