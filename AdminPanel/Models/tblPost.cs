using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel.Models
{
    public class tblPost : DatabaseObject
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public tblCategory Category { get; set; }
    }
}
