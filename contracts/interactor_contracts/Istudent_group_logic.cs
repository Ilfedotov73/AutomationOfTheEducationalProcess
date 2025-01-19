using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts { 
    public interface Istudent_group_logic {
        public List<student_group_binding_model> get_student_group_list(student_group_search_model? search_model);
        public student_group_binding_model? get_student_group_info(student_group_search_model search_model);
    }
}
