using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public class tblMainCategory : DatabaseObject
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public IEnumerable<tblCategory> Categories { get; }
    }
}
