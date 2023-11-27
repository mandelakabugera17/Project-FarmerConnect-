using Microsoft.VisualBasic;

namespace FarmerConnect.Model
{
    public class KnowledgeSharing
    {
        public int? Id { get; set; }
        public int? user_id { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public DateTime? timestamp { get; set; }

        public KnowledgeSharing()
        {
        }

        public KnowledgeSharing(int? id, int? user_id, string? title, string? content, DateTime timestamp)
        {
            Id = id;
            this.user_id = user_id;
            this.title = title;
            this.content = content;
            this.timestamp = timestamp;
        }
    }
}
