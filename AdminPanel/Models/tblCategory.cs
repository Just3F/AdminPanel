using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel.Models
{
    public class tblCategory : DatabaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long? MainCategoryId { get; set; }
        [ForeignKey("MainCategoryId")]
        public tblMainCategory MainCategory { get; set; }
    }
}
