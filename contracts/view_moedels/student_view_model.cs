using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.view_moedels {
    public class student_view_model {
        public int id { get; set; }
        public string fio { get; set; } = string.Empty;
        public int group_id { get; set; }
        public int grade_book_num { get; set; }
    }
}
