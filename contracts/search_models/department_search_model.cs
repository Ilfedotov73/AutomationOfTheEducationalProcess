using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.search_models {
    public class department_search_model {
        public int? id { get; set; }
        public string? name { get; set; }
        public int? faculty_id { get; set; }
    }
}
