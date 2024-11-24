using data_models.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.binding_models {
    public class student_binding_model : Istudent {
        public int id { get; set; }
        public string fio { get; set; } = string.Empty;
        public int group_id { get; set; }
        public int grade_book_num { get; set; }
    }
}
