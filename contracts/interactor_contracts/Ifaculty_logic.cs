using contracts.binding_models;
using contracts.search_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts.interactor_contracts {
    public interface Ifaculty_logic {
        public List<faculty_binding_model> get_faculty_list();
        public faculty_binding_model? get_faculty_info(faculty_search_model search_model);
    }
}
