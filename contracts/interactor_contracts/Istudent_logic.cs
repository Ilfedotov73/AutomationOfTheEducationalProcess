﻿using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Istudent_logic {
        public List<student_binding_model> get_student_list(student_search_model? search_model);
        public student_binding_model? get_student_info(student_search_model search_model);
    }
}
