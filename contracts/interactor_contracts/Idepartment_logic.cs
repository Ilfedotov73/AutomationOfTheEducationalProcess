using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Idepartment_logic {
        public List<department_binding_model> get_department_list();
        public List<department_binding_model> get_department_filltered_list(department_search_model search_model);
        public department_binding_model get_department_info(department_search_model search_model);
    }
}
