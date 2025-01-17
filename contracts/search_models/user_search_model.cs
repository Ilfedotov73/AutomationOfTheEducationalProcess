using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.search_models {
    public class user_search_model {
        public int? id { get; set; }
        public string? fio { get; set; }
        public int? department_id { get; set; }
        public int? position_id { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }
}
