using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.storage_contracts {
    public interface Istudent_storage {
        public List<student_binding_model> get_student_filltered_list(student_search_model search_model);
        public student_binding_model get_student_info(student_search_model search_model);
    }
}
