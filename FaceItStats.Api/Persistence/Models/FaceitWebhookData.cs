
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceItStats.Api.Persistence.Models
{
    [Table(nameof(FaceitWebhookData))]
    public class FaceitWebhookData
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public FaceitWebhookData()
        {

        }

        public FaceitWebhookData(string content)
        {
            this.Content = content;
        }
    }
}
