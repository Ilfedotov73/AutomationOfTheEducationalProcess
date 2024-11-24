using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.search_models {
    public class direction_search_model {
        public int? id { get; set; }
        public string? full_name { get; set; }
        public string? alt_name { get; set; }
        public int? department_id { get; set; }
    }
}
