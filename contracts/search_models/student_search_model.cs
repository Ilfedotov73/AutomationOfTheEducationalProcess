using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.search_models {
    public class student_search_model {
        public int? id { get; set; }
        public string? fio { get; set; }
        public int? group_id { get; set; }
        public int? grade_book_num { get; }
    }
}
