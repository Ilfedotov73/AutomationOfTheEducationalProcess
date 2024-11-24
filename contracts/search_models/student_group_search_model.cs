using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.search_models {
    public class student_group_search_model {
        public int? id { get; set; }
        public int? direction_id { get; set; }
        public int? course_num { get; }
    }
}
